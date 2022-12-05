using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Simulator
{
    public struct MicroInstructionData
    {
        public byte Zero;
        public byte Carry;
    }

    public class MicroDisassembly
    {
        protected readonly Dictionary<uint, MicroInstructionData> m_microInstructionLookup = new Dictionary<uint, MicroInstructionData>();

        protected MachineState m_machineState;

        public MicroDisassembly(MachineState machineState) => m_machineState = machineState;

        public void FillDisassemblyListView(ListView listView, Dictionary<uint, List<uint>> microSourceBreakpoints)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            byte phase = m_machineState.Phase;
            uint currentMicroaddress = m_machineState.GetCurrentMicroaddress();
            for (byte b = 0; b < 2; b = (byte)(b + 1)) {
                m_microInstructionLookup.Clear();
                for (byte b2 = 0; b2 < 2; b2 = (byte)(b2 + 1)) {
                    for (byte b3 = 0; b3 < 2; b3 = (byte)(b3 + 1)) {
                        uint microinstruction = m_machineState.GetMicroinstruction((uint)(m_machineState.ExecutingInstruction * 8 + b2 * 4 + b3 * 2 + b));
                        if (!m_microInstructionLookup.ContainsKey(microinstruction)) {
                            m_microInstructionLookup[microinstruction] = default;
                        }
                        MicroInstructionData value = m_microInstructionLookup[microinstruction];
                        value.Carry = (byte)(value.Carry | (1 << b2));
                        value.Zero = (byte)(value.Zero | (1 << b3));
                        m_microInstructionLookup[microinstruction] = value;
                    }
                }
                bool flag = true;
                Dictionary<uint, MicroInstructionData>.Enumerator enumerator = m_microInstructionLookup.GetEnumerator();
                while (enumerator.MoveNext()) {
                    KeyValuePair<uint, MicroInstructionData> current = enumerator.Current;
                    uint key = (uint)((m_machineState.ExecutingInstruction << 3) | b);
                    uint num = (uint)((current.Value.Carry << 2) | current.Value.Zero);
                    ListViewItem listViewItem = new ListViewItem {
                        Tag = new KeyValuePair<uint, uint>(key, num),
                        BackColor = (b % 2 == 0) ? Color.White : Color.FromArgb(240, 240, 240)
                    };
                    string text = "";
                    text += ConditionString(current.Value.Carry, "C", isActiveLow: true);
                    text += ConditionString(current.Value.Zero, "Z", isActiveLow: false);
                    if (text.EndsWith("&")) {
                        text = "if (" + text.Substring(0, text.Length - 1) + ") ";
                        if (text.Length % 2 == 1)
                            text += " ";
                    }
                    string text2 = MicroOpDescription(current.Key, b);
                    if (text2.StartsWith("A <- A; "))
                        text2 = text2.Substring(8);
                    listViewItem.Text = $"{b:X1} {text}{text2}";
                    if (b == phase && (current.Value.Carry & (1 << (int)((currentMicroaddress >> 2) & 1))) != 0
                        && (current.Value.Zero & (1 << (int)((currentMicroaddress >> 1) & 1))) != 0) {
                        listViewItem.ImageKey = "arrow";
                        if (microSourceBreakpoints.TryGetValue(key, out var msb)) {
                            if (msb.IndexOf(num) != -1)
                                listViewItem.ImageKey = "breakpoint with arrow";
                        }
                    } else if (microSourceBreakpoints.TryGetValue(key, out var msb)) {
                        if (msb.IndexOf(num) != -1)
                            listViewItem.ImageKey = "breakpoint";
                    }
                    flag = false;
                    listView.Items.Add(listViewItem);
                }
                if (flag)
                    break;
            }
            listView.EndUpdate();
        }

        protected static string ConditionString(byte bits, string name, bool isActiveLow)
        {
            if ((bits == 1 && !isActiveLow) || (bits == 2 && isActiveLow))
                return "!" + name + "&";
            if ((bits == 2 && !isActiveLow) || (bits == 1 && isActiveLow))
                return name + "&";
            return "";
        }

        protected static string MicroOpDescription(uint microOp, byte phase)
        {
            byte b = (byte)MachineState.FieldValue(microOp, MachineState.BitPos.kNotCarryIn, 1);
            uint num = MachineState.FieldValue(microOp, MachineState.BitPos.kS3, 4);
            string text = "";
            string text3 = "";
            if (phase == 0)
                text += "Fetch=ROM(PC)";
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kCSRAM) && MachineState.IsBitOne(microOp, MachineState.BitPos.kWERAM))
                text3 = "RAM(imm<<8|ROM(PC))";
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kOEIn))
                text3 = "IN(imm)";
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kOEImm))
                text3 = "imm";
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kOEALU))
                text3 = "????";
            string text2 = "";
            if (MachineState.IsBitOne(microOp, MachineState.BitPos.kM)) {
                switch (num) {
                    case 0: text2 += "~A"; break;
                    case 1: text2 += "~(A|" + text3 + ")"; break;
                    case 3: text2 += "0"; break;
                    case 5: text2 += "~" + text3; break;
                    case 6: text2 += "A^" + text3; break;
                    case 10: text2 += text3; break;
                    case 11: text2 += "A&" + text3; break;
                    case 12: text2 += "$F"; break;
                    case 14: text2 += "A|" + text3; break;
                    case 15: text2 += "A"; break;
                    default: text2 += $"ALU(A,{text3},${num:X1},{b},1)"; break;
                }
            } else {
                switch (num) {
                    case 0: text2 += (b == 0) ? "A+1" : "A"; break;
                    case 3: text2 += (b == 0) ? "0" : "-1"; break;
                    case 6: text2 += (b == 0) ? ("A-" + text3) : ("A-" + text3 + "-1"); break;
                    case 9: text2 += (b == 0) ? ("A+" + text3 + "+1") : ("A+" + text3); break;
                    case 12: text2 += (b == 0) ? "A+A+1" : "A+A"; break;
                    case 15: text2 += (b == 0) ? "A" : "A-1"; break;
                    default: text2 += "ALU(A," + text3 + "," + $"${num:X1},{b},0" + ")"; break;
                }
            }
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kOEALU))
                text3 = text2;
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kLoadA))
                text += "A=" + text2;
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kLoadOut))
                text += "OUT(imm)=" + text3;
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kCSRAM) && MachineState.IsBitZero(microOp, MachineState.BitPos.kWERAM))
                text += "RAM(imm<<8|ROM(PC))=" + text3;
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kLoadFlags))
                if (text.Length == 0)
                    text = text2;
            text += "; loadFlags";
            if (MachineState.IsBitZero(microOp, MachineState.BitPos.kNotLoadPC))
                text += "; PC=imm<<8|ROM(PC)";
            else if (MachineState.IsBitOne(microOp, MachineState.BitPos.kIncPC))
                text += "; PC++";
            return text;
        }
    }
}
