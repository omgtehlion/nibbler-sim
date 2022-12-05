using System;
using System.IO;
using System.Windows.Forms;

namespace Simulator
{
    public class MachineState
    {
        public enum BitPos
        {
            kLoadOut,
            kOEImm,
            kOEIn,
            kOEALU,
            kWERAM,
            kCSRAM,
            kS0,
            kS1,
            kS2,
            kS3,
            kM,
            kNotCarryIn,
            kLoadFlags,
            kLoadA,
            kNotLoadPC,
            kIncPC,
        }

        const int s_programRomSize = 4096;
        const int s_ramSize = 4096;
        const int s_microromSize = 2048;

        protected byte[] m_programROM = new byte[s_programRomSize];
        protected byte[] m_RAM = new byte[s_ramSize];
        protected byte m_buttonState = 15;
        protected uint[] m_microrom = new uint[s_microromSize];
        public LCD m_lcd = new LCD();
        protected uint m_startAddress;
        public byte Out1;
        public byte Out0;
        public bool FlagNotZero;
        public bool FlagNotCarry;
        public uint RegPC;
        public byte Fetch;
        public byte RegA;
        public byte Phase;

        public MachineState(string programRomPath, string microRomPath, uint bootAddress)
        {
            m_startAddress = bootAddress;
            for (uint num = 0u; num < m_microrom.Length; num++)
                m_microrom[num] = 0u;
            for (int i = 0; i < 2; i++) {
                if (!LoadMicrorom(microRomPath, i)) {
                    Application.Exit();
                    return;
                }
            }
            if (LoadProgramRom(programRomPath))
                Reset();
        }

        public void Reset()
        {
            Phase = 0;
            RegPC = m_startAddress;
        }

        public uint GetMicroinstruction(uint microAddress) => m_microrom[microAddress];

        public uint GetCurrentMicroaddress()
        {
            uint num = (uint)(CurrentInstruction * 8 + Phase);
            if (FlagNotCarry)
                num += 4;
            if (FlagNotZero)
                num += 2;
            return num;
        }

        public uint CurrentMicroinstruction => m_microrom[GetCurrentMicroaddress()];

        public byte CurrentInstruction => (byte)(Fetch >> 4);

        public uint CurrentMemoryAddress => (uint)(((Fetch & 0xF) << 8) | m_programROM[RegPC]);

        public byte ExecutingInstruction => Phase == 1 ? CurrentInstruction : (byte)(m_programROM[RegPC] >> 4);

        public byte CurrentImmediate => (byte)(Fetch & 0xF);

        public byte PeekRAM(uint address) => m_RAM[address & 0xFFF];

        public byte PeekROM(uint address) => m_programROM[address & 0xFFF];

        public void SetButton(int index, bool pressed)
            => m_buttonState = pressed ? (byte)(m_buttonState & ~(1 << index)) : (byte)(m_buttonState | (1 << index));

        public uint MicroStep()
        {
            uint result = 255u;
            uint currentMicroinstruction = CurrentMicroinstruction;
            if (Phase == 0) {
                Fetch = m_programROM[RegPC];
            }
            byte b = RunALU(currentMicroinstruction, out bool carryOut, out _);
            if (IsBitZero(currentMicroinstruction, BitPos.kLoadFlags)) {
                FlagNotCarry = carryOut;
                FlagNotZero = b != 0;
            }
            if (IsBitZero(currentMicroinstruction, BitPos.kLoadA)) {
                RegA = b;
            }
            byte b2 = 0;
            int num = 0;
            if (IsBitZero(currentMicroinstruction, BitPos.kOEImm)) {
                b2 = CurrentImmediate;
                num++;
            }
            if (IsBitZero(currentMicroinstruction, BitPos.kCSRAM) && IsBitOne(currentMicroinstruction, BitPos.kWERAM)) {
                b2 = (byte)(m_RAM[CurrentMemoryAddress] & 0xF);
                num++;
            }
            if (IsBitZero(currentMicroinstruction, BitPos.kOEIn)) {
                b2 = m_buttonState;
                num++;
            }
            if (IsBitZero(currentMicroinstruction, BitPos.kOEALU)) {
                b2 = (byte)(b & 0xF);
                num++;
            }
            if (num > 1)
                MessageBox.Show("too many data bus drivers");
            if (IsBitZero(currentMicroinstruction, BitPos.kLoadOut)) {
                byte currentImmediate = CurrentImmediate;
                if ((currentImmediate & 1) == 0) {
                    if ((Out0 & 1) == 1 && (b2 & 1) == 0)
                        m_lcd.WriteNibble((uint)((Out0 & 2) >> 1), Out1);
                    Out0 = b2;
                }
                if ((currentImmediate & 2) == 0)
                    Out1 = b2;
            }
            if (IsBitZero(currentMicroinstruction, BitPos.kCSRAM) && IsBitZero(currentMicroinstruction, BitPos.kWERAM)) {
                m_RAM[CurrentMemoryAddress] = b2;
                result = CurrentImmediate;
            }
            if (IsBitZero(currentMicroinstruction, BitPos.kNotLoadPC))
                RegPC = CurrentMemoryAddress;
            else if (IsBitOne(currentMicroinstruction, BitPos.kIncPC))
                RegPC = (RegPC + 1) & 0xFFF;
            Phase ^= 1;
            return result;
        }

        public static bool IsBitZero(uint bitfield, BitPos pos) => (bitfield & (1u << (int)pos)) == 0;

        public static bool IsBitOne(uint bitfield, BitPos pos) => (bitfield & (1u << (int)pos)) != 0;

        public static uint FieldValue(uint bitfield, BitPos pos, int len)
            => (bitfield >> (int)(pos - len + 1)) & ((1u << len) - 1);

        protected bool LoadMicrorom(string romPath, int romIndex)
        {
            var result = false;
            try {
                using (var binaryReader = new BinaryReader(File.OpenRead($"{romPath}/microcode_{romIndex}.bin"), System.Text.Encoding.Default)) {
                    var num = 0;
                    while (binaryReader.PeekChar() != -1)
                        m_microrom[num++] |= (uint)binaryReader.ReadByte() << romIndex * 8;
                    if (num != s_microromSize)
                        throw new ApplicationException("wrong microrom size");
                }
                result = true;
            } catch (Exception ex) {
                MessageBox.Show($"Could not load microcode_{romIndex}.bin: " + ex);
            }
            return result;
        }

        protected bool LoadProgramRom(string romPath)
        {
            m_programROM = File.ReadAllBytes(romPath);
            if (m_programROM.Length != s_programRomSize) {
                MessageBox.Show("wrong program ROM size");
                return false;
            }
            return true;
        }

        protected byte RunALU(uint microOp, out bool carryOut, out bool overflowOut)
        {
            carryOut = false;
            byte a = RegA;
            byte b = 0;
            if (IsBitZero(microOp, BitPos.kOEImm))
                b = CurrentImmediate;
            if (IsBitZero(microOp, BitPos.kCSRAM) && IsBitOne(microOp, BitPos.kWERAM))
                b = (byte)(m_RAM[CurrentMemoryAddress] & 0xF);
            if (IsBitZero(microOp, BitPos.kOEIn))
                b = m_buttonState;
            if (IsBitZero(microOp, BitPos.kOEALU))
                b = 0;
            byte c = (byte)FieldValue(microOp, BitPos.kNotCarryIn, 1);
            uint function = FieldValue(microOp, BitPos.kS3, 4);
            byte result = 0;
            if (IsBitOne(microOp, BitPos.kM)) {
                // logical mode
                switch (function) {
                    case 0: result = (byte)~a; break;
                    case 1: result = (byte)~(a | b); break;
                    case 2: result = (byte)(~a & b); break;
                    case 3: result = 0; break;
                    case 4: result = (byte)~(a & b); break;
                    case 5: result = (byte)~b; break;
                    case 6: result = (byte)(a ^ b); break;
                    case 7: result = (byte)(a ^ ~b); break;
                    case 8: result = (byte)(~a | b); break;
                    case 9: result = (byte)(~(a ^ b)); break;
                    case 10: result = b; break;
                    case 11: result = (byte)(a & b); break;
                    case 12: result = 0xFF; break;
                    case 13: result = (byte)(a | ~b); break;
                    case 14: result = (byte)(a | b); break;
                    case 15: result = a; break;
                }
                result &= 0xF;
                carryOut = false;
                overflowOut = result == 0xF;
            } else {
                // arithmetic mode
                int tempResult = 0;
                switch (function) {
                    case 0: tempResult = a + 1 - c; carryOut = tempResult < 16; break;
                    case 1: tempResult = (a | b) + 1 - c; carryOut = tempResult < 16; break;
                    case 2: tempResult = (a | ~b) + 1 - c; carryOut = tempResult < 16; break;
                    case 3: tempResult = -c; carryOut = tempResult < 0; break;
                    case 4: tempResult = a + (a & ~b) + 1 - c; carryOut = tempResult < 16; break;
                    case 5: tempResult = (a | b) + (a & ~b) + 1 - c; carryOut = tempResult < 16; break;
                    case 6: tempResult = a - b - c; carryOut = tempResult < 0; break;
                    case 7: tempResult = (a & ~b) - c; carryOut = tempResult < 0; break;
                    case 8: tempResult = a + (a & b) + 1 - c; carryOut = tempResult < 16; break;
                    case 9: tempResult = a + b + 1 - c; carryOut = tempResult < 16; break;
                    case 10: tempResult = (a | b) + (a & b) + 1 - c; carryOut = tempResult < 16; break;
                    case 11: tempResult = (a & b) - c; carryOut = tempResult < 0; break;
                    case 12: tempResult = a + a + 1 - c; carryOut = tempResult < 16; break;
                    case 13: tempResult = (a | b) + a + 1 - c; carryOut = tempResult < 16; break;
                    case 14: tempResult = (a | ~b) + a + 1 - c; carryOut = tempResult < 16; break;
                    case 15: tempResult = a - c; carryOut = tempResult < 0; break;
                }
                result = (byte)(tempResult & 0xF);
                overflowOut = result == 0xF;
            }
            return (byte)(result & 0xF);
        }
    }
}
