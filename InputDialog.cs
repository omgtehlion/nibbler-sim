using System;
using System.Windows.Forms;

namespace Simulator
{
    public class InputDialog : Form
    {
        private Label m_inputLabel;

        private TextBox m_inputBox;

        private Button m_cancelButton;

        private Button m_OKButton;

        public InputDialog()
        {
            InitializeComponent();
        }

        public string Prompt { set { m_inputLabel.Text = value; } }

        public string InputBoxText { set { m_inputBox.Text = value; } }

        public string GetInputBoxText()
        {
            return m_inputBox.Text;
        }

        private void InitializeComponent()
        {
            m_inputLabel = new System.Windows.Forms.Label();
            m_inputBox = new System.Windows.Forms.TextBox();
            m_cancelButton = new System.Windows.Forms.Button();
            m_OKButton = new System.Windows.Forms.Button();
            SuspendLayout();
            System.Drawing.Point location = new System.Drawing.Point(13, 13);
            m_inputLabel.Location = location;
            m_inputLabel.Name = "m_inputLabel";
            System.Drawing.Size size = new System.Drawing.Size(152, 23);
            m_inputLabel.Size = size;
            m_inputLabel.TabIndex = 0;
            m_inputLabel.Text = "input label";
            m_inputLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            System.Drawing.Point location2 = new System.Drawing.Point(171, 10);
            m_inputBox.Location = location2;
            m_inputBox.Name = "m_inputBox";
            System.Drawing.Size size2 = new System.Drawing.Size(100, 20);
            m_inputBox.Size = size2;
            m_inputBox.TabIndex = 1;
            System.Drawing.Point location3 = new System.Drawing.Point(16, 53);
            m_cancelButton.Location = location3;
            m_cancelButton.Name = "m_cancelButton";
            System.Drawing.Size size3 = new System.Drawing.Size(75, 23);
            m_cancelButton.Size = size3;
            m_cancelButton.TabIndex = 2;
            m_cancelButton.Text = "Cancel";
            m_cancelButton.UseVisualStyleBackColor = true;
            m_cancelButton.Click += new System.EventHandler(m_cancelButton_Click);
            System.Drawing.Point location4 = new System.Drawing.Point(196, 53);
            m_OKButton.Location = location4;
            m_OKButton.Name = "m_OKButton";
            System.Drawing.Size size4 = new System.Drawing.Size(75, 23);
            m_OKButton.Size = size4;
            m_OKButton.TabIndex = 3;
            m_OKButton.Text = "OK";
            m_OKButton.UseVisualStyleBackColor = true;
            m_OKButton.Click += new System.EventHandler(m_OKButton_Click);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(284, 88);
            base.Controls.Add(m_OKButton);
            base.Controls.Add(m_cancelButton);
            base.Controls.Add(m_inputBox);
            base.Controls.Add(m_inputLabel);
            base.Name = "InputDialog";
            Text = "InputDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        private void m_OKButton_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            Close();
        }

        private void m_cancelButton_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
