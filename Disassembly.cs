using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Simulator
{
    public struct InstructionData
    {
        public int Length;
        public string Mnemo;
        public InstructionData(string mnemo, int length) { Mnemo = mnemo; Length = length; }
    }

    public struct FileAndLine
    {
        public string filename;
        public int lineNumber;
        public string sourceLabel;
    }

    public class Disassembly
    {
        protected readonly InstructionData[] m_opcodeLookup = new[] {
            new InstructionData("JC", 2),
            new InstructionData("JNC", 2),
            new InstructionData("CMPI", 1),
            new InstructionData("CMPM", 2),
            new InstructionData("LIT", 1),
            new InstructionData("IN", 1),
            new InstructionData("LD", 2),
            new InstructionData("ST", 2),
            new InstructionData("JZ", 2),
            new InstructionData("JNZ", 2),
            new InstructionData("ADCI", 1),
            new InstructionData("ADCM", 2),
            new InstructionData("JMP", 2),
            new InstructionData("OUT", 1),
            new InstructionData("NORI", 1),
            new InstructionData("NORM", 2)
        };

        protected MachineState m_machineState;

        public Disassembly(MachineState machineState) => m_machineState = machineState;

        Dictionary<string, ListViewItem[]> parsedCache = new Dictionary<string, ListViewItem[]>();

        ListViewItem[] ParseAsm(string filename, Dictionary<uint, FileAndLine> addressToSource, uint startAddress)
        {
            if (parsedCache.TryGetValue(filename, out var result))
                return result;
            var resultLst = new List<ListViewItem>();
            var lineNumber = 1;
            var nextAddress = startAddress;
            foreach (var text in File.ReadLines(filename)) {
                var listViewItem = new ListViewItem(text.Replace("\t", "    ")) { Tag = nextAddress };
                resultLst.Add(listViewItem);
                lineNumber++;
                int searchCount = 0;
                uint testAddress = nextAddress;
                while (true) {
                    if (++searchCount > 32)
                        break;
                    testAddress++;
                    if (addressToSource.TryGetValue(testAddress, out FileAndLine value2) &&
                        value2.lineNumber == lineNumber && value2.filename == filename) {
                        nextAddress = testAddress;
                        break;
                    }
                }
                if (searchCount > 32) {
                    foreach (var current in addressToSource) {
                        var value3 = current.Value;
                        if (value3.lineNumber == lineNumber && value3.filename == filename) {
                            nextAddress = current.Key;
                            break;
                        }
                    }
                }
            }
            parsedCache.Add(filename, result = resultLst.ToArray());
            return result;
        }

        public void FillDisassemblyListView(ListView listView, uint pcAddress, HashSet<uint> sourceBreakpoints,
            Dictionary<uint, FileAndLine> addressToSource, Dictionary<string, uint> sourceStartAddress, bool ignoreSource)
        {
            int currentInstrIndex = -1;
            uint nextAddress = pcAddress;
            listView.BeginUpdate();
            bool foundSource = false;
            if (!ignoreSource && addressToSource.TryGetValue(nextAddress, out FileAndLine value)) {
                try {
                    var parsed = ParseAsm(value.filename, addressToSource, sourceStartAddress[value.filename]);
                    if (listView.Tag != parsed) {
                        listView.Tag = parsed;
                        listView.Items.Clear();
                        listView.Items.AddRange(parsed);
                    }
                    currentInstrIndex = UpdatePics(listView, pcAddress, sourceBreakpoints);
                    foundSource = true;
                } catch (Exception ex) {
                    MessageBox.Show($"Couldn't open source file {value.filename}: {ex}", "File Error");
                }
            }
            if (!foundSource) {
                listView.Tag = null;
                listView.Items.Clear();
                for (int i = 0; i < 300; i++) {
                    byte b0 = m_machineState.PeekROM(nextAddress);
                    byte b1 = m_machineState.PeekROM(nextAddress + 1);
                    var lineItem = new ListViewItem($"{nextAddress:X3} ") { Tag = nextAddress };
                    if (addressToSource.TryGetValue(nextAddress, out var fileAndLine)) {
                        lineItem.Text += fileAndLine.sourceLabel;
                        for (int j = fileAndLine.sourceLabel.Length; j < 21; j++)
                            lineItem.Text += " ";
                    } else {
                        lineItem.Text += "                     ";
                    }
                    var opcode = (byte)(b0 >> 4);
                    var instructionLength = 1u;
                    if (m_opcodeLookup.Length > opcode) {
                        var data = m_opcodeLookup[opcode];
                        if (data.Length == 2) {
                            uint destination = (uint)(((m_machineState.PeekROM(nextAddress) & 0xF) << 8)
                                | m_machineState.PeekROM(nextAddress + 1));
                            lineItem.Text += $"{b0:X2}{b1:X2}    {data.Mnemo} ${destination:X3}";
                        } else if (data.Length == 1) {
                            lineItem.Text += $"{b0:X2}      {data.Mnemo} ${m_machineState.PeekROM(nextAddress) & 0xF:X1}\r\n";
                        }
                        instructionLength = (uint)data.Length;
                    } else {
                        lineItem.Text += "???";
                    }
                    listView.Items.Add(lineItem);
                    nextAddress += instructionLength;
                }
                currentInstrIndex = UpdatePics(listView, pcAddress, sourceBreakpoints);
            }
            if (currentInstrIndex >= 0)
                listView.EnsureVisible(currentInstrIndex);
            listView.EndUpdate();
        }

        static int UpdatePics(ListView listView, uint pcAddress, HashSet<uint> sourceBreakpoints)
        {
            uint setBreakpointAddr = 0u;
            int currentInstructionIndex = -1;
            foreach (ListViewItem listViewItem in listView.Items) {
                var addr = (uint)listViewItem.Tag;
                var isBp = setBreakpointAddr != addr && sourceBreakpoints.Contains(addr);
                if (addr == pcAddress && currentInstructionIndex == -1) {
                    currentInstructionIndex = listViewItem.Index;
                    if (isBp) {
                        //listViewItem.ImageKey = "breakpoint with arrow";
                        listViewItem.ImageIndex = 2;
                        setBreakpointAddr = addr;
                    } else {
                        //listViewItem.ImageKey = "arrow";
                        listViewItem.ImageIndex = 0;
                    }
                } else if (isBp) {
                    //listViewItem.ImageKey = "breakpoint";
                    listViewItem.ImageIndex = 1;
                    setBreakpointAddr = addr;
                } else {
                    //listViewItem.ImageKey = null;
                    listViewItem.ImageIndex = -1;
                }
                if (!isBp)
                    setBreakpointAddr = 0u;
            }
            return currentInstructionIndex;
        }

        public static void UpdateDisassemblyListView(ListView listView, uint pcAddress, HashSet<uint> sourceBreakpoints,
            bool scrollToCurrentInstruction)
        {
            listView.BeginUpdate();
            var currentInstructionIndex = UpdatePics(listView, pcAddress, sourceBreakpoints);
            if (scrollToCurrentInstruction && currentInstructionIndex >= 0)
                listView.EnsureVisible(currentInstructionIndex);
            listView.EndUpdate();
        }

        public string GetOpcodeDescription(byte opcode) => m_opcodeLookup.Length > opcode ? m_opcodeLookup[opcode].Mnemo : "???";
    }
}
