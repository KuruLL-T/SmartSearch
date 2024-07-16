using SmartSearch.App.Models;
using SmartSearch.App.RabbitMQ;

namespace SmartSearch.App
{
    public partial class SmartSearchForm : Form
    {
        private readonly Producer _rabbitMQProducer;
        public SmartSearchForm()
        {
            InitializeComponent();
            _rabbitMQProducer = new Producer();
        }

        private void SetGuid(TextBox textBox)
        {
            textBox.Text = Guid.NewGuid().ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sendButton.BringToFront();
            SetGuid(textBox2);
            SetGuid(textBox4);
            SetGuid(textBox6);
            SetGuid(textBox13);
        }

        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]))!;
        }

        void ClearForm()
        {
            IEnumerable<TextBoxBase> textBoxCollection = modelsControl.SelectedTab!.Controls.OfType<TextBoxBase>();
            IEnumerable<ComboBox> comboboxCollection = modelsControl.SelectedTab!.Controls.OfType<ComboBox>();
            IEnumerable<PictureBox> pictureCollection = modelsControl.SelectedTab!.Controls.OfType<PictureBox>();
            foreach (TextBoxBase item in textBoxCollection)
            {
                item.Clear();
                if (item.ReadOnly)
                {
                    SetGuid((TextBox)item);
                }
            }

            foreach (ComboBox item in comboboxCollection)
            {
                item.SelectedIndex = 0;
            }

            foreach (PictureBox item in pictureCollection)
            {
                item.Image = null;
            }
        }

        private IModel CreateModel()
        {
            IModel model = modelsControl.SelectedIndex switch
            {
                0 => CreateService(),
                1 => CreateSearchItemType(),
                2 => CreateUser(),
                3 => CreateSearchItem(),
                _ => throw new NotImplementedException(),
            };
            return model;
        }

        private SearchItem CreateSearchItem()
        {
            SearchItem searchItem = new SearchItem
            {
                Data = new SearchItemData
                {
                    ExternalId = Guid.Parse(textBox13.Text),
                    TypeId = (comboBox4.SelectedItem as SearchItemType).Data.TypeId,
                    Description = richTextBox2.Text,
                    Header = textBox11.Text,
                    Image = new Models.Image
                    {
                        Extension = Path.GetExtension(pictureBox2.ImageLocation),
                        Data = Convert.ToBase64String(ImageToByte(pictureBox2.Image))
                    },
                    ImageString = $"data:image/{Path.GetExtension(pictureBox2.Text).Trim('.')};base64,{Convert.ToBase64String(ImageToByte(pictureBox2.Image))}",
                    Link = textBox10.Text,
                    AccessRights = richTextBox4.Text,
                }
            };
            ModelsStorage.searchItems.Add(searchItem);
            return searchItem;
        }

        private User CreateUser()
        {
            User user = new User
            {
                Data = new UserData
                {
                    UserType = comboBox1.SelectedIndex,
                    ExternalId = Guid.Parse(textBox6.Text),
                    TypeId = (comboBox3.SelectedItem as SearchItemType).Data.TypeId,
                    Header = textBox8.Text,
                    Description = richTextBox1.Text,
                    Image = new Models.Image
                    {
                        Extension = Path.GetExtension(pictureBox1.ImageLocation),
                        Data = Convert.ToBase64String(ImageToByte(pictureBox1.Image))
                    },
                    ImageString = $"data:image/{Path.GetExtension(pictureBox1.Text).Trim('.')};base64,{Convert.ToBase64String(ImageToByte(pictureBox1.Image))}",
                    Link = textBox9.Text,
                    AccessRights = richTextBox5.Text
                }
            };
            ModelsStorage.users.Add(user);
            return user;
        }

        private SearchItemType CreateSearchItemType()
        {
            SearchItemType searchItemType = new SearchItemType
            {
                Data = new SearchItemTypeData
                {
                    TypeId = Guid.Parse(textBox6.Text),
                    Name = textBox3.Text,
                    ServiceDocumentId = (comboBox2.SelectedItem as Service).Data.Id,
                    Priority = Convert.ToInt32(numericUpDown1.Value),
                    ServiceId = (comboBox2.SelectedItem as Service).Data.Id
                }
            };
            ModelsStorage.searchItemTypes.Add(searchItemType);
            return searchItemType;
        }

        private Service CreateService()
        {
            Service service = new Service
            {
                Data = new ServiceData
                {
                    Id = Guid.Parse(textBox2.Text),
                    Name = textBox1.Text,
                }
            };
            ModelsStorage.services.Add(service);
            return service;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            IModel model = CreateModel();
            UpdateListBox();
            ClearForm();
            string routingKey = modelsControl.SelectedTab!.Tag!.ToString()!;
            _rabbitMQProducer.SendMessage("smartsearch_exchange", $"add_{routingKey}", model);
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully added", "Success", MessageBoxButtons.OK);
        }

        private void SaveImage(PictureBox pictureBox)
        {
            try
            {
                Utils.SaveImage(pictureBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveImage(pictureBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveImage(pictureBox2);
        }

        void UpdateListBox()
        {
            switch (modelsControl.SelectedIndex)
            {
                case 0:
                    {
                        listBox2.Items.Clear();
                        var services = ModelsStorage.services;
                        if (services.Count() > 0)
                        {
                            listBox2.Items.AddRange(services.ToArray());
                        }
                        break;
                    }
                case 1:
                    {
                        listBox3.Items.Clear();
                        var searchItemTypes = ModelsStorage.searchItemTypes;
                        if (searchItemTypes.Count() > 0)
                        {
                            listBox3.Items.AddRange(searchItemTypes.ToArray());
                        }
                        break;
                    }
                case 2:
                    {
                        listBox4.Items.Clear();
                        var users = ModelsStorage.users;
                        if (users.Count() > 0)
                        {
                            listBox4.Items.AddRange(users.ToArray());
                        }
                        break;
                    }
                case 3:
                    {
                        listBox5.Items.Clear();
                        var searchItems = ModelsStorage.searchItems;
                        if (searchItems.Count() > 0)
                        {
                            listBox5.Items.AddRange(searchItems.ToArray());
                        }
                        break;
                    }
            }
        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                Service service = (Service)listBox2.SelectedItem;
                textBox1.Text = service.Data.Name;
                textBox2.Text = service.Data.Id.ToString();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                SearchItemType searchItemType = (SearchItemType)listBox3.SelectedItem;
                textBox3.Text = searchItemType.Data.Name;
                textBox4.Text = searchItemType.Data.TypeId.ToString();
                numericUpDown1.Value = searchItemType.Data.Priority;
                comboBox2.SelectedItem = ModelsStorage.services.Find(service => service.Data.Id == searchItemType.Data.ServiceId);
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem != null)
            {
                User user = (User)listBox4.SelectedItem;
                comboBox1.SelectedIndex = user.Data.UserType;
                textBox6.Text = user.Data.ExternalId.ToString();
                comboBox3.SelectedItem = ModelsStorage.searchItemTypes.Find(searchItemTypes => searchItemTypes.Data.TypeId == user.Data.TypeId);
                textBox8.Text = user.Data.Header;
                richTextBox1.Text = user.Data.Description;
                pictureBox1.Image = new Bitmap(Utils.ByteToImage(Convert.FromBase64String(user.Data.Image.Data)));
                textBox9.Text = user.Data.Link;
                richTextBox5.Text = user.Data.AccessRights;
            }
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox5.SelectedItem != null)
            {
                SearchItem searchItem = (SearchItem)listBox5.SelectedItem;
                textBox13.Text = searchItem.Data.ExternalId.ToString();
                comboBox4.SelectedItem = ModelsStorage.searchItemTypes.Find(searchItemTypes => searchItemTypes.Data.TypeId == searchItem.Data.TypeId);
                textBox11.Text = searchItem.Data.Header;
                richTextBox2.Text = searchItem.Data.Description;
                pictureBox2.Image = new Bitmap(Utils.ByteToImage(Convert.FromBase64String(searchItem.Data.Image.Data)));
                textBox10.Text = searchItem.Data.Link;
                richTextBox4.Text = searchItem.Data.AccessRights;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox2)) return;
            Service service = ModelsStorage.services.Find(service => service == (Service)listBox2.SelectedItem);
            service.Data.Name = textBox1.Text;
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "update_service", service);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully updated", "Success", MessageBoxButtons.OK);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox2)) return;
            Service service = ModelsStorage.services.Find(service => service == (Service)listBox2.SelectedItem);
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "delete_service", service);
            ModelsStorage.services.Remove(service);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully deleted", "Success", MessageBoxButtons.OK);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox3)) return;
            SearchItemType searchItemType = ModelsStorage.searchItemTypes.Find(searchItemType => searchItemType == (SearchItemType)listBox3.SelectedItem);
            searchItemType.Data.Name = textBox3.Text;
            searchItemType.Data.ServiceId = (comboBox2.SelectedItem as Service).Data.Id;
            searchItemType.Data.ServiceDocumentId = (comboBox2.SelectedItem as Service).Data.Id;
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "update_type", searchItemType);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully updated", "Success", MessageBoxButtons.OK);
        }

        bool checkSelectedItem(ListBox listBox)
        {
            if (listBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose an item");
                return false;
            }
            return true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox4)) return;
            User user = ModelsStorage.users.Find(user => user == (User)listBox4.SelectedItem);
            user.Data = new UserData
            {
                UserType = comboBox1.SelectedIndex,
                ExternalId = Guid.Parse(textBox6.Text),
                TypeId = (comboBox3.SelectedItem as SearchItemType).Data.TypeId,
                Header = textBox8.Text,
                Description = richTextBox1.Text,
                Image = new Models.Image
                {
                    Extension = Path.GetExtension(pictureBox1.ImageLocation),
                    Data = Convert.ToBase64String(ImageToByte(pictureBox1.Image))
                },
                ImageString = $"data:image/{Path.GetExtension(pictureBox1.Text).Trim('.')};base64,{Convert.ToBase64String(ImageToByte(pictureBox1.Image))}",
                Link = textBox9.Text,
                AccessRights = richTextBox5.Text
            };
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "update_user", user);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully updated", "Success", MessageBoxButtons.OK);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox5)) return;
            SearchItem searchItem = ModelsStorage.searchItems.Find(searchItem => searchItem == (SearchItem)listBox5.SelectedItem);
            searchItem.Data = new SearchItemData
            {
                ExternalId = Guid.Parse(textBox13.Text),
                TypeId = (comboBox4.SelectedItem as SearchItemType).Data.TypeId,
                Description = richTextBox2.Text,
                Header = textBox11.Text,
                Image = new Models.Image
                {
                    Extension = Path.GetExtension(pictureBox2.ImageLocation),
                    Data = Convert.ToBase64String(ImageToByte(pictureBox2.Image))
                },
                ImageString = $"data:image/{Path.GetExtension(pictureBox2.Text).Trim('.')};base64,{Convert.ToBase64String(ImageToByte(pictureBox2.Image))}",
                Link = textBox10.Text,
                AccessRights = richTextBox4.Text
            };
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "update_searchitem", searchItem);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully updated", "Success", MessageBoxButtons.OK);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox5)) return;
            SearchItem searchItem = ModelsStorage.searchItems.Find(searchItem => searchItem == (SearchItem)listBox5.SelectedItem);
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "delete_searchitem", searchItem);
            ModelsStorage.searchItems.Remove(searchItem);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully deleted", "Success", MessageBoxButtons.OK);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox3)) return;
            SearchItemType searchItemTypes = ModelsStorage.searchItemTypes.Find(searchItemTypes => searchItemTypes == (SearchItemType)listBox3.SelectedItem);
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "delete_type", searchItemTypes);
            ModelsStorage.searchItemTypes.Remove(searchItemTypes);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully deleted", "Success", MessageBoxButtons.OK);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            comboBox2.DataSource = null;
            comboBox2.DataSource = ModelsStorage.services;
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            comboBox3.DataSource = ModelsStorage.searchItemTypes;
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            comboBox4.DataSource = ModelsStorage.searchItemTypes;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            modelsControl.Enabled = false;
            loadingLabel.Visible = true;
            if (!checkSelectedItem(listBox4)) return;
            User user = ModelsStorage.users.Find(user => user == (User)listBox4.SelectedItem);
            _rabbitMQProducer.SendMessage("smartsearch_exchange", "delete_user", user);
            ModelsStorage.users.Remove(user);
            UpdateListBox();
            ClearForm();
            loadingLabel.Visible = false;
            modelsControl.Enabled = true;
            MessageBox.Show("Successfully deleted", "Success", MessageBoxButtons.OK);
        }
    }
}
