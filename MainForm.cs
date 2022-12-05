using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Simulator
{
    public partial class MainForm : Form
    {
        public uint m_memoryBreakpointAddress;
        private MachineState m_machineState;
        private Disassembly m_disassembler;
        private MicroDisassembly m_microDisassembler;
        private uint m_memoryViewAddress;
        private uint m_currentInstructionAddress;
        private uint m_sourceDisassemblyStartAddress;
        private uint m_stepOverDestinationAddress;
        private uint m_bootAddress;
        private uint m_currentStackFrameAddress;
        private string m_bootImage;
        private bool m_ignoreSource;
        private bool m_running;
        private int m_refreshCycles = 100000;
        private DateTime m_lastUpdateTime;
        private TimeSpan m_lastFrameDuration;
        private uint m_clocksSinceReset;
        private readonly HashSet<uint> m_sourceBreakpoints = new HashSet<uint>();
        private readonly Dictionary<uint, List<uint>> m_microSourceBreakpoints = new Dictionary<uint, List<uint>>();
        private readonly Dictionary<uint, FileAndLine> m_addressToSource = new Dictionary<uint, FileAndLine>();
        private readonly Dictionary<string, uint> m_sourceStartAddress = new Dictionary<string, uint>();
        private readonly List<uint> m_stackFrames = new List<uint>();
        private static float s_clockSpeedMHz = 2.4576f;
        DateTime LastTick;

        public MainForm()
        {
            InitializeComponent();
            m_ignoreSource = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.Idle += OnIdle;
            InitializeSimulation();
            MainForm_SizeChanged(sender, e);
        }

        private void OnIdle(object sender, EventArgs e)
        {
            var now = DateTime.UtcNow;
            if (m_running) {
                var toSleep = 2 * m_refreshCycles / s_clockSpeedMHz / 1e3 - (now - LastTick).TotalMilliseconds;
                if (toSleep >= 1)
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(toSleep));
                LastTick = now;
                uint num = (uint)m_refreshCycles;
                uint num2 = 0u;
                while (num != 0) {
                    bool flag = false;
                    bool flag2 = false;
                    do {
                        uint num3 = m_machineState.MicroStep();
                        num2++;
                        if (num3 == m_memoryBreakpointAddress) {
                            MessageBox.Show($"Memory breakpoint {num3:X1}");
                            flag2 = true;
                            break;
                        }
                        if (CurrentMicroaddressIsAtBreakpoint()) {
                            flag = true;
                            break;
                        }
                    } while (m_machineState.Phase != 0);
                    if (m_machineState.Phase == 0) {
                        m_currentInstructionAddress = m_machineState.RegPC;
                        m_currentStackFrameAddress = m_currentInstructionAddress;
                        flag2 = RetireInstruction();
                    }
                    if (flag || m_sourceBreakpoints.Contains(m_currentInstructionAddress)
                        || m_currentInstructionAddress == m_stepOverDestinationAddress || flag2) {
                        m_running = false;
                        m_pauseButton.Enabled = false;
                        m_continueButtton.Enabled = true;
                        m_stepButton.Enabled = true;
                        m_microStepButton.Enabled = true;
                        m_clearBreakpointsButton.Enabled = true;
                        m_stepOverDestinationAddress = 0xFFFFFFu;
                        m_statusLabel.Text = "Breakpoint hit";
                        m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
                        break;
                    }
                    num--;
                }
                m_clocksSinceReset += num2;
                m_statusLabel.Text = $"Running: {20.0 * m_refreshCycles / m_lastFrameDuration.Ticks:f2} MHz";
                m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
                RefreshData();
            }
            now = DateTime.UtcNow;
            m_lastFrameDuration = now - m_lastUpdateTime;
            m_lastUpdateTime = now;
        }

        private void m_memoryAddressTextBox_Validated(object sender, EventArgs e)
            => FillMemoryTextBox(m_memoryViewTextBox, m_memoryViewAddress, 0u, highlight: false);

        private void m_sourceListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_running)
                return;
            var listViewItem = ((ListView)sender).GetItemAt(e.X, e.Y);
            if (listViewItem != null) {
                uint key = (uint)listViewItem.Tag;
                if (m_sourceBreakpoints.Contains(key))
                    m_sourceBreakpoints.Remove(key);
                else
                    m_sourceBreakpoints.Add(key);
            }
            RefreshDisassemblyView(scrollToCurrentInstruction: false);
        }

        private void m_microSourceListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_running)
                return;
            ListViewItem listViewItem = ((ListView)sender).GetItemAt(e.X, e.Y);
            if (listViewItem != null) {
                var valueType = (KeyValuePair<uint, uint>)listViewItem.Tag;
                if (m_microSourceBreakpoints.ContainsKey(valueType.Key)) {
                    bool flag = false;
                    List<uint> list = m_microSourceBreakpoints[valueType.Key];
                    List<uint>.Enumerator enumerator = list.GetEnumerator();
                    while (enumerator.MoveNext()) {
                        uint current = enumerator.Current;
                        if (current == valueType.Value) {
                            list.Remove(current);
                            if (list.Count == 0)
                                m_microSourceBreakpoints.Remove(valueType.Key);
                            flag = true;
                            break;
                        }
                    }
                    if (!flag) {
                        list.Add(valueType.Value);
                    }
                } else {
                    m_microSourceBreakpoints[valueType.Key] = new List<uint> { valueType.Value };
                }
            }
            RefreshMicroDisassemblyView();
        }

        private void m_clearBreakpointsButton_Click(object sender, EventArgs e)
        {
            m_sourceBreakpoints.Clear();
            m_microSourceBreakpoints.Clear();
            RefreshDisassemblyView(scrollToCurrentInstruction: false);
            RefreshMicroDisassemblyView();
        }

        private void m_sourceListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => e.Item.Selected = false;

        private void m_microSourceListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => e.Item.Selected = false;

        private void m_microStepButton_Click(object sender, EventArgs e)
        {
            m_machineState.MicroStep();
            m_clocksSinceReset++;
            if (m_machineState.Phase == 0) {
                m_currentInstructionAddress = m_machineState.RegPC;
                RetireInstruction();
            }
            m_currentStackFrameAddress = m_currentInstructionAddress;
            m_statusLabel.Text = "Stopped";
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
            RefreshData();
        }

        private void m_stepButton_Click(object sender, EventArgs e)
        {
            do {
                m_machineState.MicroStep();
                m_clocksSinceReset++;
            }
            while (m_machineState.Phase != 0);
            m_currentInstructionAddress = m_machineState.RegPC;
            m_currentStackFrameAddress = m_currentInstructionAddress;
            RetireInstruction();
            m_statusLabel.Text = "Stopped";
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
            RefreshData();
        }

        private void m_resetButton_Click(object sender, EventArgs e)
        {
            m_machineState.Reset();
            m_pauseButton.Enabled = false;
            m_continueButtton.Enabled = true;
            m_stepButton.Enabled = true;
            m_microStepButton.Enabled = true;
            m_clearBreakpointsButton.Enabled = true;
            m_running = false;
            m_currentInstructionAddress = m_machineState.RegPC;
            m_currentStackFrameAddress = m_currentInstructionAddress;
            m_stepOverDestinationAddress = 0xFFFFFFu;
            m_clocksSinceReset = 0u;
            m_statusLabel.Text = "Stopped";
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
            m_stackFrames.Clear();
            RefreshData();
        }

        private void m_continueButtton_Click(object sender, EventArgs e)
        {
            m_pauseButton.Enabled = true;
            m_continueButtton.Enabled = false;
            m_stepButton.Enabled = false;
            m_microStepButton.Enabled = false;
            m_clearBreakpointsButton.Enabled = false;
            m_statusLabel.Text = "Running";
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
            m_running = true;
        }

        private void m_pauseButton_Click(object sender, EventArgs e)
        {
            m_continueButtton.Enabled = true;
            m_continueButtton.Focus();
            m_pauseButton.Enabled = false;
            m_stepButton.Enabled = true;
            m_microStepButton.Enabled = true;
            m_clearBreakpointsButton.Enabled = true;
            m_running = false;
            m_stepOverDestinationAddress = 0xFFFFFFu;
            m_statusLabel.Text = "Stopped";
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
            RefreshData();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Title = "Select Symbol File",
                Filter = "symbol files (*.sym)|*.sym|label files (*.lbl)|*.lbl",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                LoadSourceLabels(openFileDialog.FileName);
            }
        }

        private void openProgramROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_pauseButton_Click(sender, e);
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Title = "Select Program File",
                Filter = "bin files (*.bin)|*.bin",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                m_bootImage = openFileDialog.FileName;
                Application.UserAppDataRegistry.SetValue("Boot Image", m_bootImage);
                InitializeSimulation();
                //m_machineState = new MachineState(openFileDialog.FileName, ".", m_bootAddress);
                RefreshData();
                RefreshDisassemblyView(scrollToCurrentInstruction: true);
            }
        }

        private void InitializeSimulation()
        {
            m_tooltip.SetToolTip(m_continueButtton, "Run (F5)");
            m_tooltip.SetToolTip(m_stepButton, "Step Over (F10)");
            m_tooltip.SetToolTip(m_microStepButton, "Micro-Step");
            m_tooltip.SetToolTip(m_pauseButton, "Pause");
            m_tooltip.SetToolTip(m_resetButton, "Reset");
            try {
                m_bootImage = (string)Application.UserAppDataRegistry.GetValue("Boot Image");
                if (m_bootImage == null) {
                    m_bootImage = ".\\microchess.bin";
                }
            } catch {
                m_bootImage = ".\\microchess.bin";
            }
            try {
                string text = (string)Application.UserAppDataRegistry.GetValue("Boot Address");
                m_bootAddress = uint.Parse(text);
            } catch {
                m_bootAddress = 0u;
            }
            //try {
            //    m_refreshCycles = (int)(uint)Application.UserAppDataRegistry.GetValue("Refresh Cycles");
            //} catch {
            //    m_refreshCycles = 200000;
            //}
            m_pauseButton.Enabled = false;
            m_statusLabel.Text = "Stopped";
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / s_clockSpeedMHz:N1} us";
            m_running = false;
            m_memoryViewAddress = 0u;
            m_sourceDisassemblyStartAddress = uint.MaxValue;
            m_memoryBreakpointAddress = 0xFFFFFFu;
            m_machineState = new MachineState(m_bootImage, ".", m_bootAddress);
            m_disassembler = new Disassembly(m_machineState);
            m_microDisassembler = new MicroDisassembly(m_machineState);
            m_currentInstructionAddress = m_machineState.RegPC;
            m_currentStackFrameAddress = m_currentInstructionAddress;
            m_machineState.FlagNotCarry = false;
            m_machineState.FlagNotZero = false;
            Text = "Nibbler - " + m_bootImage.Split('\\').Last();
            LoadSourceLabels(Path.ChangeExtension(m_bootImage, ".sym"));
            m_stepOverDestinationAddress = 0xFFFFFFu;
            m_clocksSinceReset = 0u;
            m_stackFrames.Clear();
            radioButton1_CheckedChanged(null, null);
            RefreshData();
        }

        private bool CurrentMicroaddressIsAtBreakpoint()
        {
            uint currentMicroaddress = m_machineState.GetCurrentMicroaddress();
            uint key = currentMicroaddress >> 5;
            if (m_microSourceBreakpoints.ContainsKey(key)) {
                foreach (var current in m_microSourceBreakpoints[key])
                    if ((current & (1u << (int)((currentMicroaddress >> 1) & 1))) != 0
                        && (current & (1u << (int)(((currentMicroaddress >> 2) & 1) + 2))) != 0) {
                        return true;
                    }
                return false;
            }
            return false;
        }

        private void RefreshData()
        {
            if (m_machineState == null)
                return;
            m_aLabel.Text = $"{m_machineState.RegA:X1}";
            m_pcLabel.Text = $"{m_machineState.RegPC:X3}";
            m_fetchLabel.Text = $"{m_machineState.Fetch:X2}";
            m_phaseLabel.Text = $"{m_machineState.Phase:X1}";
            m_out0Label.Text = $"{m_machineState.Out0:X1}";
            m_out1Label.Text = $"{m_machineState.Out1:X1}";
            m_opLabel.Text = m_disassembler.GetOpcodeDescription(m_machineState.ExecutingInstruction);
            m_cRadioButton.Checked = !m_machineState.FlagNotCarry;
            m_eRadioButton.Checked = !m_machineState.FlagNotZero;
            FillMemoryTextBox(m_memoryViewTextBox, m_memoryViewAddress, 0u, highlight: false);
            RefreshDisassemblyView(scrollToCurrentInstruction: true);
            RefreshMicroDisassemblyView();
            pictureBox1.Image = m_machineState.m_lcd.Render();
            m_microDisassemblyViewTitle.Text = m_disassembler.GetOpcodeDescription(m_machineState.ExecutingInstruction) + " - Micro-Disassembly";
        }

        private void RefreshDisassemblyView(bool scrollToCurrentInstruction)
        {
            uint num = (uint)Math.Max(0, (int)((m_currentStackFrameAddress - 50) / 50u * 50));
            if (num == m_sourceDisassemblyStartAddress) {
                Disassembly.UpdateDisassemblyListView(m_sourceListView, m_currentInstructionAddress, m_sourceBreakpoints, scrollToCurrentInstruction);
                return;
            }
            m_sourceDisassemblyStartAddress = num;
            m_disassembler.FillDisassemblyListView(m_sourceListView, m_currentInstructionAddress, m_sourceBreakpoints, m_addressToSource, m_sourceStartAddress, m_ignoreSource);
        }

        private void RefreshMicroDisassemblyView()
        {
            m_microDisassembler.FillDisassemblyListView(m_microSourceListView, m_microSourceBreakpoints);
        }

        private void FillMemoryTextBox(TextBox textBox, uint startAddress, uint highlightAddress, bool highlight)
        {
            string text = null;
            startAddress = 0u;
            for (int i = 0; i < 128; i++) {
                text += $"{(uint)((int)startAddress + i * 16):X3}";
                for (int j = 0; j < 16; j++) {
                    text = ((!highlight || (int)startAddress + i * 16 + j != (int)highlightAddress) ? (text + " ") : (text + ">"));
                    byte b = m_machineState.PeekRAM((uint)((int)startAddress + i * 16 + j));
                    text += $"{b:X1}";
                }
                if (i < 127)
                    text += "\r\n";
            }
            textBox.Text = text;
        }

        private void LoadSourceLabels(string filename)
        {
            m_addressToSource.Clear();
            m_sourceStartAddress.Clear();
            if (!File.Exists(filename)) {
                if (filename.EndsWith(".sym")) {
                    filename = filename.Substring(0, filename.LastIndexOf(".")) + ".lbl";
                } else if (filename.EndsWith(".lbl")) {
                    filename = filename.Substring(0, filename.LastIndexOf(".")) + ".sym";
                }
            }
            bool flag = filename.EndsWith(".lbl");
            string text = ".\\";
            int num = filename.LastIndexOf('\\');
            if (num >= 0) {
                text = filename.Substring(0, num + 1);
            }
            try {
                using (StreamReader streamReader = new StreamReader(filename)) {
                    while (true) {
                        string text2 = streamReader.ReadLine();
                        if (text2 == null) {
                            break;
                        } else {
                            if (text2.StartsWith("L_")) {
                                int length = text2.IndexOf(';');
                                text2 = text2.Substring(0, length);
                                string[] array = text2.Split('=');
                                string text3 = array[0].Substring(2, array[0].Length - 3);
                                uint num2 = uint.Parse(array[1].Substring(1), NumberStyles.HexNumber);
                                int num3 = text3.LastIndexOf('_');
                                string text4 = text3.Substring(0, num3);
                                int num4 = int.Parse(text3.Substring(num3 + 1));
                                num3 = text4.LastIndexOf('_');
                                if (num3 >= 0) {
                                    text4 = text4.Substring(0, num3) + "." + text4.Substring(num3 + 1);
                                }
                                text4 = text + text4;
                                if (m_addressToSource.TryGetValue(num2, out FileAndLine value)) {
                                    if (value.filename == null) {
                                        value.filename = text4;
                                        value.lineNumber = num4;
                                        m_addressToSource[num2] = value;
                                    } else if (value.filename.Equals(text4)) {
                                        if (num4 > value.lineNumber) {
                                            value.lineNumber = num4;
                                            m_addressToSource[num2] = value;
                                        }
                                    } else if (num4 < value.lineNumber) {
                                        value.filename = text4;
                                        value.lineNumber = num4;
                                        m_addressToSource[num2] = value;
                                    }
                                } else {
                                    value.filename = text4;
                                    value.lineNumber = num4;
                                    value.sourceLabel = "";
                                    m_addressToSource.Add(num2, value);
                                }
                                if (m_sourceStartAddress.TryGetValue(text4, out uint value2)) {
                                    if (num2 < value2) {
                                        m_sourceStartAddress[text4] = num2;
                                    }
                                } else {
                                    m_sourceStartAddress[text4] = num2;
                                }
                            } else {
                                if (flag) {
                                    string[] array2 = text2.Split('.');
                                    uint key = uint.Parse(array2[0].Substring(3, 6), NumberStyles.HexNumber);
                                    m_addressToSource.TryGetValue(key, out FileAndLine value3);
                                    value3.sourceLabel = array2[1].Trim();
                                    value3.filename = "unknown";
                                    value3.lineNumber = 1;
                                    m_addressToSource[key] = value3;
                                } else {
                                    int num5 = text2.IndexOf(';');
                                    if (num5 >= 0)
                                        text2 = text2.Substring(0, num5);
                                    string[] array3 = text2.Split('=');
                                    uint key = uint.Parse(array3[1].Substring(1), NumberStyles.HexNumber);
                                    m_addressToSource.TryGetValue(key, out FileAndLine value3);
                                    value3.sourceLabel = array3[0].Trim();
                                    m_addressToSource[key] = value3;
                                }
                            }
                        }
                    }
                    m_disassembler.FillDisassemblyListView(m_sourceListView, m_currentInstructionAddress, m_sourceBreakpoints, m_addressToSource, m_sourceStartAddress, m_ignoreSource);
                }
            } catch {
                MessageBox.Show($"Couldn't open symbol file {filename}", "File Error");
            }
        }

        private bool RetireInstruction() => false;

        private void m_ShowDisassemblyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            m_ignoreSource = m_ShowDisassemblyCheckbox.Checked;
            m_disassembler.FillDisassemblyListView(m_sourceListView, m_currentInstructionAddress, m_sourceBreakpoints, m_addressToSource, m_sourceStartAddress, m_ignoreSource);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            InputDialog inputDialog = new InputDialog {
                Text = "Set Program Start Address",
                Prompt = "Start Address (hex)",
                InputBoxText = $"{m_bootAddress:X6}"
            };
            DialogResult dialogResult = inputDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                m_bootAddress = Convert.ToUInt32(inputDialog.GetInputBoxText(), 16);
                Application.UserAppDataRegistry.SetValue("Boot Address", m_bootAddress);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            var inputDialog = new InputDialog {
                Text = "Set Memory Breakpoint",
                Prompt = "Breakpoint Address (hex)",
                InputBoxText = $"{m_memoryBreakpointAddress:X6}"
            };
            DialogResult dialogResult = inputDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
                m_memoryBreakpointAddress = Convert.ToUInt32(inputDialog.GetInputBoxText(), 16);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e) => m_memoryBreakpointAddress = 0xFFFFFFu;

        private void m_buttonLeft_MouseDown(object sender, MouseEventArgs e) => m_machineState.SetButton(0, pressed: true);

        private void m_buttonLeft_MouseUp(object sender, MouseEventArgs e) => m_machineState.SetButton(0, pressed: false);

        private void m_buttonRight_MouseDown(object sender, MouseEventArgs e) => m_machineState.SetButton(1, pressed: true);

        private void m_buttonRight_MouseUp(object sender, MouseEventArgs e) => m_machineState.SetButton(1, pressed: false);

        private void m_buttonDown_MouseDown(object sender, MouseEventArgs e) => m_machineState.SetButton(2, pressed: true);

        private void m_buttonDown_MouseUp(object sender, MouseEventArgs e) => m_machineState.SetButton(2, pressed: false);

        private void m_buttonUp_MouseDown(object sender, MouseEventArgs e) => m_machineState.SetButton(3, pressed: true);

        private void m_buttonUp_MouseUp(object sender, MouseEventArgs e) => m_machineState.SetButton(3, pressed: false);

        private void m_resetTimeButton_Click(object sender, EventArgs e)
        {
            m_clocksSinceReset = 0u;
            m_timeLabel.Text = $"Time: {m_clocksSinceReset / 2u:N0} us";
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        public static int Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            Application.Run(new MainForm());
            return 0;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F10) {
                m_stepButton_Click(sender, e);
                e.Handled = true;
            } else if (e.KeyCode == Keys.F5) {
                m_continueButtton_Click(sender, e);
                e.Handled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            m_machineState.m_lcd.SetLines(radioButton1.Checked ? 2 : 4);
            RefreshData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_machineState.m_lcd.Reset();
            RefreshData();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            columnHeader1.Width = m_sourceListView.Width - 30;
        }
    }
}
