namespace SmartSearch.App
{
    partial class SmartSearchForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            sendButton = new Button();
            modelsControl = new TabControl();
            servicePage = new TabPage();
            button4 = new Button();
            listBox2 = new ListBox();
            button5 = new Button();
            searchItemTypePage = new TabPage();
            numericUpDown1 = new NumericUpDown();
            label24 = new Label();
            comboBox2 = new ComboBox();
            button6 = new Button();
            button7 = new Button();
            listBox3 = new ListBox();
            userPage = new TabPage();
            richTextBox5 = new RichTextBox();
            label22 = new Label();
            comboBox3 = new ComboBox();
            updateButton = new Button();
            deleteButton = new Button();
            listBox4 = new ListBox();
            pictureBox1 = new PictureBox();
            label12 = new Label();
            textBox9 = new TextBox();
            button2 = new Button();
            comboBox1 = new ComboBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label4 = new Label();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            textBox8 = new TextBox();
            textBox6 = new TextBox();
            searchItemPage = new TabPage();
            richTextBox4 = new RichTextBox();
            label14 = new Label();
            comboBox4 = new ComboBox();
            button10 = new Button();
            button11 = new Button();
            listBox5 = new ListBox();
            pictureBox2 = new PictureBox();
            label13 = new Label();
            textBox10 = new TextBox();
            button3 = new Button();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            richTextBox2 = new RichTextBox();
            textBox11 = new TextBox();
            textBox13 = new TextBox();
            loadingLabel = new Label();
            openFileDialog1 = new OpenFileDialog();
            label20 = new Label();
            modelsControl.SuspendLayout();
            servicePage.SuspendLayout();
            searchItemTypePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            userPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            searchItemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(271, 149);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(233, 27);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(271, 181);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(233, 27);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(277, 133);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(233, 27);
            textBox3.TabIndex = 2;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(277, 165);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(233, 27);
            textBox4.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(199, 149);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 5;
            label2.Text = "name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(199, 185);
            label3.Name = "label3";
            label3.Size = new Size(22, 20);
            label3.TabIndex = 6;
            label3.Text = "id";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(134, 168);
            label5.Name = "label5";
            label5.Size = new Size(22, 20);
            label5.TabIndex = 9;
            label5.Text = "id";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(134, 133);
            label6.Name = "label6";
            label6.Size = new Size(46, 20);
            label6.TabIndex = 8;
            label6.Text = "name";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(134, 231);
            label7.Name = "label7";
            label7.Size = new Size(94, 20);
            label7.TabIndex = 11;
            label7.Text = "serviceName";
            // 
            // sendButton
            // 
            sendButton.BackColor = Color.DarkGreen;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            sendButton.ForeColor = Color.White;
            sendButton.Location = new Point(12, 553);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(94, 37);
            sendButton.TabIndex = 12;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = false;
            sendButton.Click += button1_Click;
            // 
            // modelsControl
            // 
            modelsControl.Controls.Add(servicePage);
            modelsControl.Controls.Add(searchItemTypePage);
            modelsControl.Controls.Add(userPage);
            modelsControl.Controls.Add(searchItemPage);
            modelsControl.Location = new Point(12, 12);
            modelsControl.Name = "modelsControl";
            modelsControl.SelectedIndex = 0;
            modelsControl.Size = new Size(720, 535);
            modelsControl.TabIndex = 15;
            modelsControl.Tag = "";
            // 
            // servicePage
            // 
            servicePage.Controls.Add(button4);
            servicePage.Controls.Add(listBox2);
            servicePage.Controls.Add(button5);
            servicePage.Controls.Add(textBox1);
            servicePage.Controls.Add(textBox2);
            servicePage.Controls.Add(label2);
            servicePage.Controls.Add(label3);
            servicePage.Location = new Point(4, 29);
            servicePage.Name = "servicePage";
            servicePage.Padding = new Padding(3);
            servicePage.Size = new Size(712, 502);
            servicePage.TabIndex = 0;
            servicePage.Tag = "service";
            servicePage.Text = "Service";
            servicePage.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.BackColor = Color.DarkOrange;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button4.ForeColor = Color.White;
            button4.Location = new Point(106, 459);
            button4.Name = "button4";
            button4.Size = new Size(94, 37);
            button4.TabIndex = 16;
            button4.Text = "Update";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(206, 392);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(500, 104);
            listBox2.TabIndex = 13;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged_1;
            // 
            // button5
            // 
            button5.BackColor = Color.DarkRed;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button5.ForeColor = Color.White;
            button5.Location = new Point(6, 459);
            button5.Name = "button5";
            button5.Size = new Size(94, 37);
            button5.TabIndex = 17;
            button5.Text = "Delete";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // searchItemTypePage
            // 
            searchItemTypePage.Controls.Add(numericUpDown1);
            searchItemTypePage.Controls.Add(label24);
            searchItemTypePage.Controls.Add(comboBox2);
            searchItemTypePage.Controls.Add(button6);
            searchItemTypePage.Controls.Add(button7);
            searchItemTypePage.Controls.Add(listBox3);
            searchItemTypePage.Controls.Add(textBox3);
            searchItemTypePage.Controls.Add(textBox4);
            searchItemTypePage.Controls.Add(label6);
            searchItemTypePage.Controls.Add(label5);
            searchItemTypePage.Controls.Add(label7);
            searchItemTypePage.Location = new Point(4, 29);
            searchItemTypePage.Name = "searchItemTypePage";
            searchItemTypePage.Padding = new Padding(3);
            searchItemTypePage.Size = new Size(712, 502);
            searchItemTypePage.TabIndex = 1;
            searchItemTypePage.Tag = "type";
            searchItemTypePage.Text = "Search Item Type";
            searchItemTypePage.UseVisualStyleBackColor = true;
            searchItemTypePage.Enter += tabPage2_Enter;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(278, 198);
            numericUpDown1.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(232, 27);
            numericUpDown1.TabIndex = 26;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(134, 198);
            label24.Name = "label24";
            label24.Size = new Size(57, 20);
            label24.TabIndex = 25;
            label24.Text = "priority";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(278, 231);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(232, 28);
            comboBox2.TabIndex = 21;
            // 
            // button6
            // 
            button6.BackColor = Color.DarkOrange;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button6.ForeColor = Color.White;
            button6.Location = new Point(103, 459);
            button6.Name = "button6";
            button6.Size = new Size(94, 37);
            button6.TabIndex = 18;
            button6.Text = "Update";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.DarkRed;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button7.ForeColor = Color.White;
            button7.Location = new Point(3, 459);
            button7.Name = "button7";
            button7.Size = new Size(94, 37);
            button7.TabIndex = 19;
            button7.Text = "Delete";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.Location = new Point(203, 412);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(503, 84);
            listBox3.TabIndex = 14;
            listBox3.SelectedIndexChanged += listBox3_SelectedIndexChanged;
            // 
            // userPage
            // 
            userPage.Controls.Add(richTextBox5);
            userPage.Controls.Add(label22);
            userPage.Controls.Add(comboBox3);
            userPage.Controls.Add(updateButton);
            userPage.Controls.Add(deleteButton);
            userPage.Controls.Add(listBox4);
            userPage.Controls.Add(pictureBox1);
            userPage.Controls.Add(label12);
            userPage.Controls.Add(textBox9);
            userPage.Controls.Add(button2);
            userPage.Controls.Add(comboBox1);
            userPage.Controls.Add(label11);
            userPage.Controls.Add(label10);
            userPage.Controls.Add(label9);
            userPage.Controls.Add(label8);
            userPage.Controls.Add(label4);
            userPage.Controls.Add(label1);
            userPage.Controls.Add(richTextBox1);
            userPage.Controls.Add(textBox8);
            userPage.Controls.Add(textBox6);
            userPage.Location = new Point(4, 29);
            userPage.Name = "userPage";
            userPage.Size = new Size(712, 502);
            userPage.TabIndex = 2;
            userPage.Tag = "user";
            userPage.Text = "User";
            userPage.UseVisualStyleBackColor = true;
            userPage.Enter += tabPage3_Enter;
            // 
            // richTextBox5
            // 
            richTextBox5.Location = new Point(260, 309);
            richTextBox5.Name = "richTextBox5";
            richTextBox5.Size = new Size(315, 96);
            richTextBox5.TabIndex = 37;
            richTextBox5.Text = "";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(169, 309);
            label22.Name = "label22";
            label22.Size = new Size(85, 20);
            label22.TabIndex = 36;
            label22.Text = "acess rights";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(256, 89);
            comboBox3.Margin = new Padding(3, 4, 3, 4);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(318, 28);
            comboBox3.TabIndex = 22;
            // 
            // updateButton
            // 
            updateButton.BackColor = Color.DarkOrange;
            updateButton.FlatStyle = FlatStyle.Flat;
            updateButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            updateButton.ForeColor = Color.White;
            updateButton.Location = new Point(102, 458);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(94, 37);
            updateButton.TabIndex = 20;
            updateButton.Text = "Update";
            updateButton.UseVisualStyleBackColor = false;
            updateButton.Click += button8_Click;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.DarkRed;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            deleteButton.ForeColor = Color.White;
            deleteButton.Location = new Point(3, 458);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 37);
            deleteButton.TabIndex = 21;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += button9_Click;
            // 
            // listBox4
            // 
            listBox4.FormattingEnabled = true;
            listBox4.Location = new Point(202, 411);
            listBox4.Name = "listBox4";
            listBox4.Size = new Size(507, 84);
            listBox4.TabIndex = 15;
            listBox4.SelectedIndexChanged += listBox4_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(581, 309);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(127, 93);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(166, 276);
            label12.Name = "label12";
            label12.Size = new Size(32, 20);
            label12.TabIndex = 13;
            label12.Text = "link";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(257, 276);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(317, 27);
            textBox9.TabIndex = 12;
            // 
            // button2
            // 
            button2.Location = new Point(257, 241);
            button2.Name = "button2";
            button2.Size = new Size(315, 29);
            button2.TabIndex = 11;
            button2.Text = "choose image";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "student", "teacher" });
            comboBox1.Location = new Point(256, 24);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(317, 28);
            comboBox1.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(165, 24);
            label11.Name = "label11";
            label11.Size = new Size(69, 20);
            label11.TabIndex = 9;
            label11.Text = "user type";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(165, 243);
            label10.Name = "label10";
            label10.Size = new Size(51, 20);
            label10.TabIndex = 8;
            label10.Text = "image";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(165, 157);
            label9.Name = "label9";
            label9.Size = new Size(77, 20);
            label9.TabIndex = 7;
            label9.Text = "decription";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(165, 124);
            label8.Name = "label8";
            label8.Size = new Size(55, 20);
            label8.TabIndex = 6;
            label8.Text = "header";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(165, 91);
            label4.Name = "label4";
            label4.Size = new Size(51, 20);
            label4.TabIndex = 5;
            label4.Text = "typeId";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(165, 59);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 4;
            label1.Text = "externalId";
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Location = new Point(256, 157);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(317, 72);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(256, 124);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(317, 27);
            textBox8.TabIndex = 2;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(256, 56);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(316, 27);
            textBox6.TabIndex = 0;
            // 
            // searchItemPage
            // 
            searchItemPage.Controls.Add(richTextBox4);
            searchItemPage.Controls.Add(label14);
            searchItemPage.Controls.Add(comboBox4);
            searchItemPage.Controls.Add(button10);
            searchItemPage.Controls.Add(button11);
            searchItemPage.Controls.Add(listBox5);
            searchItemPage.Controls.Add(pictureBox2);
            searchItemPage.Controls.Add(label13);
            searchItemPage.Controls.Add(textBox10);
            searchItemPage.Controls.Add(button3);
            searchItemPage.Controls.Add(label15);
            searchItemPage.Controls.Add(label16);
            searchItemPage.Controls.Add(label17);
            searchItemPage.Controls.Add(label18);
            searchItemPage.Controls.Add(label19);
            searchItemPage.Controls.Add(richTextBox2);
            searchItemPage.Controls.Add(textBox11);
            searchItemPage.Controls.Add(textBox13);
            searchItemPage.Location = new Point(4, 29);
            searchItemPage.Name = "searchItemPage";
            searchItemPage.Size = new Size(712, 502);
            searchItemPage.TabIndex = 3;
            searchItemPage.Tag = "searchitem";
            searchItemPage.Text = "Search Item";
            searchItemPage.UseVisualStyleBackColor = true;
            searchItemPage.Enter += tabPage4_Enter;
            // 
            // richTextBox4
            // 
            richTextBox4.Location = new Point(258, 276);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.Size = new Size(315, 133);
            richTextBox4.TabIndex = 35;
            richTextBox4.Text = "";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(167, 276);
            label14.Name = "label14";
            label14.Size = new Size(79, 20);
            label14.TabIndex = 34;
            label14.Text = "acess right";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(258, 52);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(315, 28);
            comboBox4.TabIndex = 33;
            // 
            // button10
            // 
            button10.BackColor = Color.DarkOrange;
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button10.ForeColor = Color.White;
            button10.Location = new Point(103, 462);
            button10.Name = "button10";
            button10.Size = new Size(94, 37);
            button10.TabIndex = 31;
            button10.Text = "Update";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // button11
            // 
            button11.BackColor = Color.DarkRed;
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button11.ForeColor = Color.White;
            button11.Location = new Point(3, 462);
            button11.Name = "button11";
            button11.Size = new Size(94, 37);
            button11.TabIndex = 32;
            button11.Text = "Delete";
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
            // 
            // listBox5
            // 
            listBox5.FormattingEnabled = true;
            listBox5.Location = new Point(203, 415);
            listBox5.Name = "listBox5";
            listBox5.Size = new Size(506, 84);
            listBox5.TabIndex = 30;
            listBox5.SelectedIndexChanged += listBox5_SelectedIndexChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(582, 316);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(127, 93);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 29;
            pictureBox2.TabStop = false;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(167, 238);
            label13.Name = "label13";
            label13.Size = new Size(32, 20);
            label13.TabIndex = 28;
            label13.Text = "link";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(258, 238);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(317, 27);
            textBox10.TabIndex = 27;
            // 
            // button3
            // 
            button3.Location = new Point(258, 203);
            button3.Name = "button3";
            button3.Size = new Size(315, 29);
            button3.TabIndex = 26;
            button3.Text = "choose image";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(166, 206);
            label15.Name = "label15";
            label15.Size = new Size(51, 20);
            label15.TabIndex = 23;
            label15.Text = "image";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(166, 119);
            label16.Name = "label16";
            label16.Size = new Size(77, 20);
            label16.TabIndex = 22;
            label16.Text = "decription";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(166, 86);
            label17.Name = "label17";
            label17.Size = new Size(55, 20);
            label17.TabIndex = 21;
            label17.Text = "header";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(166, 54);
            label18.Name = "label18";
            label18.Size = new Size(51, 20);
            label18.TabIndex = 20;
            label18.Text = "typeId";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(166, 20);
            label19.Name = "label19";
            label19.Size = new Size(75, 20);
            label19.TabIndex = 19;
            label19.Text = "externalId";
            // 
            // richTextBox2
            // 
            richTextBox2.BorderStyle = BorderStyle.FixedSingle;
            richTextBox2.Location = new Point(257, 119);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(317, 72);
            richTextBox2.TabIndex = 18;
            richTextBox2.Text = "";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(257, 86);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(317, 27);
            textBox11.TabIndex = 17;
            // 
            // textBox13
            // 
            textBox13.Location = new Point(257, 20);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(317, 27);
            textBox13.TabIndex = 15;
            // 
            // loadingLabel
            // 
            loadingLabel.AutoSize = true;
            loadingLabel.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loadingLabel.Location = new Point(122, 550);
            loadingLabel.Name = "loadingLabel";
            loadingLabel.Size = new Size(146, 38);
            loadingLabel.TabIndex = 18;
            loadingLabel.Text = "Loading...";
            loadingLabel.Visible = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label20.Location = new Point(307, 569);
            label20.Name = "label20";
            label20.Size = new Size(124, 18);
            label20.TabIndex = 36;
            label20.Text = "@ made with soul";
            // 
            // SmartSearchForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(743, 598);
            Controls.Add(loadingLabel);
            Controls.Add(label20);
            Controls.Add(sendButton);
            Controls.Add(modelsControl);
            Name = "SmartSearchForm";
            Text = "SmartSearch App";
            Load += Form1_Load;
            modelsControl.ResumeLayout(false);
            servicePage.ResumeLayout(false);
            servicePage.PerformLayout();
            searchItemTypePage.ResumeLayout(false);
            searchItemTypePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            userPage.ResumeLayout(false);
            userPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            searchItemPage.ResumeLayout(false);
            searchItemPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button sendButton;
        private TabControl modelsControl;
        private TabPage servicePage;
        private TabPage searchItemTypePage;
        private TabPage userPage;
        private TabPage searchItemPage;
        private TextBox textBox6;
        private RichTextBox richTextBox1;
        private TextBox textBox8;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label4;
        private Label label1;
        private Label label12;
        private TextBox textBox9;
        private Button button2;
        private ComboBox comboBox1;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label13;
        private TextBox textBox10;
        private Button button3;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private RichTextBox richTextBox2;
        private TextBox textBox11;
        private TextBox textBox13;
        private Button button4;
        private Button button5;
        private ListBox listBox2;
        private ListBox listBox3;
        private ListBox listBox4;
        private ListBox listBox5;
        private Button button6;
        private Button button7;
        private Button updateButton;
        private Button deleteButton;
        private Button button10;
        private Button button11;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private RichTextBox richTextBox4;
        private Label label14;
        private RichTextBox richTextBox5;
        private Label label22;
        private ComboBox comboBox2;
        private Label label24;
        private Label loadingLabel;
        private Label label20;
        private NumericUpDown numericUpDown1;
    }
}
