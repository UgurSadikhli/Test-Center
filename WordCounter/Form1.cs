using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Security.Policy;
using Label = System.Reflection.Emit.Label;
using Button = System.Windows.Forms.Button;

namespace WordCounter
{
    public partial class GAME : Form
    {
        public GAME()
        {
            InitializeComponent();
            LOGIN_MENU.Dock = DockStyle.Fill;
            LOGIN_MENU.Show();
            this.Height = 585;
            this.Width = 915;
            Signup_menu.Dock = DockStyle.Fill;
            Signup_menu.Hide();
            panel1.Dock = DockStyle.Fill;
            panel1.Hide();
            panel3.Dock = DockStyle.Right;
            panel3.Hide();
            panel4.Dock = DockStyle.Right;
            panel4.Hide();
            panel6.Dock = DockStyle.Right;
            panel6.Hide();
            panel7.Dock = DockStyle.Right;
            panel7.Hide();
         
            pictureBox33.Visible= false;
            pictureBox34.Visible = false;
            pictureBox35.Visible = false;
            pictureBox36.Visible = false;


            LoadUser();
            LoadQuestion();
            foreach (var question in questions.questions_History)
            {
                History.Add(question);
            }

        }

        int c = 0;
        int a = 1;
        int scoreee = 0;
        XmlSerializer svusers = new XmlSerializer(typeof(User_list));
        XmlSerializer svqw = new XmlSerializer(typeof(Question_list));
        public List<int> lis = new List<int>();
        User_list user_List = new User_list();
      
        User user = new User();
        Question_list questions = new Question_list();
        List<Question> History = new List<Question>();
        List<Question> Exx = new List<Question>();

        public void SaveUser()
        {
            using (Stream fStream = File.Create("c:../../../users.txt"))
            {
                svusers.Serialize(fStream, user_List);
            }
        }
        public void LoadUser()
        {
            using (Stream fStream = File.OpenRead("c:../../../users.txt"))
            {
                user_List = (User_list)svusers.Deserialize(fStream);
            }

        }
        public void LoadQuestion()
        {
            using (Stream fStream = File.OpenRead("c:../../../question.txt"))
            {
                questions = (Question_list)svqw.Deserialize(fStream);
            }
        }
        public void SaveQuestion()
        {
            using (Stream fStream = File.Create("c:../../../question.txt"))
            {
                svqw.Serialize(fStream, questions);
            }
        }
        int rand()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int num = rnd.Next(100000, 900000);
            return num;
        }

        private void signup_b_Click(object sender, EventArgs e)
        {
            LOGIN_MENU.Hide();
            Signup_menu.Show();
            this.Height = 585;
            this.Width = 915;
        }

        private void signin_b_Click(object sender, EventArgs e)
        {
            int check = 0;
            foreach (var combi in user_List.users)
            {
                if (combi.Username == username_panel1.Text && combi.Password == userpassword_panel1.Text)
                {
                    user.Username = combi.Username;
                    user.Password = combi.Password;
                    user.Email = combi.Email;
                    user.Score = combi.Score;
                    user.Record = combi.Record;
                    LOGIN_MENU.Hide();
                    panel1.Show();
                    label6.Text = user.Username;
                    label16.Text = user.Score.ToString();
                    this.Width = 850;
                    this.Height = 538;
                    username_panel1.Text = "";
                    userpassword_panel1.Text = "";
                    check++;

                }
            }
                if (check == 0) { MessageBox.Show("No Such user"); }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Signup_menu.Hide();
            LOGIN_MENU.Show();
            this.Height = 585;
            this.Width = 915;
        }
        void SMTPr()
        {
            int a = rand();
            lis.Add(a);
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = "Verification code",
                Body = $"\nHello {textBox4.Text}, \nYou registered an account on [TEST CENTER], before being able\n to use your account you need to verify that this is your email\n address by writing down this unique code: [ {a.ToString()} ]"
            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "TEST CENTER");
            message.To.Add(new System.Net.Mail.MailAddress(textBox5.Text));

            client.Send(message);
        }
        void SMTPrNew()
        {
            int a = rand();
            lis.Add(a);
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                Subject = $"Your verification code {textBox1.Text}",
                Body = a.ToString()
            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "TEST CENTER");
            message.To.Add(new System.Net.Mail.MailAddress(textBox12.Text));

            client.Send(message);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            lis.Clear();
            int a = 0;
            foreach (var c in user_List.users)
            {
                if (username_panel1.Text == c.Username)
                {
                    a++;
                }
            }
            if (textBox4.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox5.Text.Contains("@") && a == 0)
            {
                user.Username += textBox4.Text;
                user.Password += textBox3.Text;
                Thread thread = new Thread(SMTPr);
                thread.Start();
                MessageBox.Show("code has been sent");
                label10.Visible = true;
                textBox6.Visible = true;
                pictureBox11.Visible = true;
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string title = "Error";
                DialogResult result = MessageBox.Show("Retry again some sing gone wrong\n **fill all gaps\n **maybe there is user with your name (try to change it)", title, buttons);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == lis.First().ToString())
                {
                    MessageBox.Show("SUCSESSFULY DONE");
                    user.Email += textBox5.Text;
                    user_List.users.Add(new User { Username = user.Username, Password = user.Password, Email = user.Email, Score = user.Score, Record = user.Record, });
                    SaveUser();
                    Signup_menu.Hide();

                    lis.Clear();
                    textBox6.Text = null;
                    textBox5.Text = null;
                    textBox4.Text = null;
                    textBox3.Text = null;
                    textBox6.Visible = false;
                    label10.Visible = false;
                    pictureBox11.Visible = false;
                    LOGIN_MENU.Show();
                    this.Height = 585;
                    this.Width = 915;
                }
            }
            catch (Exception ex) { }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            string title = "User Info";
            string message = $"Name: {user.Username}\nPassword: {user.Password}\nEmail: {user.Email}\nOverall score: {user.Score}";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var item in questions.questions_History)
            {
                if (!comboBox1.Items.Contains(item.Name_Theory))
                {
                    comboBox1.Items.Add(item.Name_Theory.ToString());
                }
                
            }
            pictureBox33.Visible = false;
            pictureBox34.Visible = true;
            pictureBox35.Visible = false;
            pictureBox36.Visible = false;
            pictureBox21.Visible = false;
            pictureBox23.Visible = false;
            pictureBox22.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            panel6.Hide();
            panel4.Hide();
            panel3.Show();
            panel7.Hide();
            panel3.Width = 620;
            panel3.Height = 538;
         

        }


        private void pictureBox12_Click(object sender, EventArgs e)
        {
            pictureBox33.Visible = true;
            pictureBox34.Visible = false;
            pictureBox35.Visible = false;
            pictureBox36.Visible = false;
            pictureBox21.Visible = false;
            pictureBox23.Visible = false;
            pictureBox22.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label16.Text=user.Score.ToString();
            panel3.Hide();
            panel4.Hide();
            panel6.Show();
            panel7.Hide();
            panel6.Width = 620;
            panel6.Height = 538;
        }



        private int durationInSeconds = 0;
        void task()
        {

            durationInSeconds = Convert.ToInt32(trackBar1.Value) * 600 / 10;
            timer1.Start();
            trackBar1.Enabled = false;
            comboBox1.Enabled = false;
        }

         void ending(int number)
        {
            user.Record.Add(scoreee);
            user.Score += user.Record.Last();
            A_q.Text = "answer";
            B_q.Text = "answer";
            C_q.Text = "answer";
            D_q.Text = "answer";
            label11.Text = "question";
            label17.Text = "0)";
            c = 0;
            a = 1;
            History.Clear();
            foreach (var i in questions.questions_History)
            {
                History.Add(i);
            }
            MessageBox.Show($"All questions have been asked !\nYour final score is ({scoreee}) out of ({number}) questions 🏁 ");

            foreach (var i in user_List.users)
            {
                if (i.Username == user.Username)
                {
                    i.Score += scoreee;
                    i.Record = user.Record;
                }
            }
            SaveUser();

            scoreee = 0;
            durationInSeconds = 0;
            tim.Text = "timer";
            timer1.Stop();
            trackBar1.Value = 0;
            trackBar1.Enabled = true;
            comboBox1.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                int number = 0;
                foreach (var question in questions.questions_History)
                {
                    if (question.Name_Theory == comboBox1.SelectedItem.ToString())
                    {
                        number++;
                    }

                }
                if (trackBar1.Value != 0)
                {

                    if (a == 1)
                    {
                        task();
                    }
                    label17.Text = a.ToString() + ")";

                    if (number > a)
                    {
                        a++;
                    }
                    c++;
                    if (number + 1 < c)
                    {

                        ending(number);
                        
                    }

                    else
                    {
                        foreach (var question in questions.questions_History)
                        {

                            if (question.Text_i == label11.Text)
                            {
                                if (radioButton1.Checked == true && question.Answers[0].IsCorrect)
                                {
                                    scoreee++;

                                }
                                else if (radioButton2.Checked == true && question.Answers[1].IsCorrect)
                                {
                                    scoreee++;

                                }
                                else if (radioButton3.Checked == true && question.Answers[2].IsCorrect)
                                {
                                    scoreee++;

                                }
                                else if (radioButton4.Checked == true && question.Answers[3].IsCorrect)
                                {
                                    scoreee++;

                                }

                            }
                        }
                        foreach (var question in History.ToList())
                        {
                            if (question.Name_Theory == comboBox1.SelectedItem.ToString())
                            {
                                groupBox3.Visible = false;
                                checkBox3.Checked =false;
                                History.Remove(question);
                                label11.Text = question.Text_i;
                                A_q.Text = question.Answers[0].Text;
                                B_q.Text = question.Answers[1].Text;
                                C_q.Text = question.Answers[2].Text;
                                D_q.Text = question.Answers[3].Text;
                                break;
                            }

                        }
                    }


                }
                else if (comboBox1.SelectedItem.ToString() == null)
                {
                    MessageBox.Show("SET SUBJECT");
                }
                else
                {
                    MessageBox.Show("SET TIME");
                }
            }catch(Exception ex) { MessageBox.Show("            •IN 'DETAILS'🠗\n1)SET SUBJECT | 2)SET TIME"); }
            
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }
        string refactor()
        {
            int i = ((durationInSeconds--)/60 );
            int b = (durationInSeconds) - (i * 60);
            return i+":"+b;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int number = 0;
            foreach (var question in questions.questions_History)
            {
                if (question.Name_Theory == comboBox1.SelectedItem.ToString())
                {
                    number++;
                }

            }
            tim.Text = refactor();
            if(durationInSeconds == 0)
            {
                ending(number);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tim.Text = trackBar1.Value.ToString();
        }

       
        bool fo = true;
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            if(fo == true)
            {
                pictureBox19.Visible = true;
                fo = false;
            }
            else if(fo == false)
            {
                pictureBox19.Visible = false;
                fo= true;
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            textBox14.Items.Clear();
            foreach (var item in questions.questions_History)
            {
                if (!textBox14.Items.Contains(item.Name_Theory))
                {
                    textBox14.Items.Add(item.Name_Theory.ToString());
                }

            }
            pictureBox33.Visible = false;
            pictureBox34.Visible = false;
            pictureBox35.Visible = true;
            pictureBox36.Visible = false;
            pictureBox21.Visible = false;
            pictureBox23.Visible = false;
            pictureBox22.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            panel6.Hide();
            panel3.Hide();
            panel4.Show();
            panel7.Hide();
            panel4.Width = 620;
            panel4.Height = 538;

        }

   
        int numeric = 0;
        void addition()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox1.Text == "empty" || textBox2.Text == "empty" || textBox7.Text == "empty" || textBox8.Text == "empty" || textBox9.Text == "empty")
            {
                textBox1.Text = "empty";
                textBox2.Text = "empty";
                textBox7.Text = "empty";
                textBox8.Text = "empty";
                textBox9.Text = "empty";
            }
            else if (radioButton5.Checked == false && radioButton6.Checked == false && radioButton7.Checked == false && radioButton8.Checked == false)
            {
                MessageBox.Show("\tWHICH IS CORRECT ? \n\tSELLECT {A B C D} ❌");
            }

            else
            {
                string teeext = "";
                bool correct = false;
                bool correct2 = false;
                bool correct3 = false;
                bool correct4 = false;

                if (radioButton5.Checked == true ? correct = true : correct = false) ;
                if (radioButton6.Checked == true ? correct2 = true : correct2 = false) ;
                if (radioButton7.Checked == true ? correct3 = true : correct3 = false) ;
                if (radioButton8.Checked == true ? correct4 = true : correct4 = false) ;

                if(textBox14.Text == null)
                {
                    teeext = textBox14.SelectedItem.ToString();
                }
                else
                {
                    teeext = textBox14.Text.ToString();
                }
                Exx.Add(
                new Question()
                {
                    Text_i = textBox1.Text.ToString(),
                    Name_Theory = teeext,
                    Answers = new List<Answer>()
                    {
                    new Answer() { Text = textBox2.Text.ToString(), IsCorrect= correct},
                    new Answer() { Text = textBox7.Text.ToString(), IsCorrect= correct2},
                    new Answer() { Text = textBox8.Text.ToString(), IsCorrect= correct3},
                    new Answer() { Text = textBox9.Text.ToString(), IsCorrect= correct4},

                    }
                });
                textBox1.Text = "";
                textBox2.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                numeric++;
                label13.Text = numeric.ToString();
            }
        }

        void coadd()
        {
            if (label13.Text == "num")
            {
                bool correct = false;
                bool correct2 = false;
                bool correct3 = false;
                bool correct4 = false;

                if (radioButton5.Checked == true ? correct = true : correct = false) ;
                if (radioButton6.Checked == true ? correct2 = true : correct2 = false) ;
                if (radioButton7.Checked == true ? correct3 = true : correct3 = false) ;
                if (radioButton8.Checked == true ? correct4 = true : correct4 = false) ;

                Exx.Add(
                new Question()
                {
                    Text_i = textBox1.Text.ToString(),
                    Answers = new List<Answer>()
                    {
                    new Answer() { Text = textBox2.Text.ToString(), IsCorrect= correct},
                    new Answer() { Text = textBox7.Text.ToString(), IsCorrect= correct2},
                    new Answer() { Text = textBox8.Text.ToString(), IsCorrect= correct3},
                    new Answer() { Text = textBox9.Text.ToString(), IsCorrect= correct4},

                    }
                });

            }
            foreach (var i in Exx)
            {
                questions.questions_History.Add(i);
                History.Add(i);
            }
            SaveQuestion();
            Exx.Clear();
            textBox1.Text = "ADDED SUCSESFULLY";
            textBox2.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
           
            numeric = 0;
            label13.Text = "0";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(addition);
            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(coadd);
            thread.Start();
        }

        void my_score()
        {
            listView1.Items.Clear();

            foreach (var item in user_List.users)
            {
                if (item.Username == user.Username)
                {
                    foreach (var i in item.Record)
                    {
                        ListViewItem lvi = new ListViewItem(item.Username);
                        lvi.SubItems.Add(i.ToString());
                        listView1.Items.Add(lvi);
                    }
                }
            }
        }
        void all_scores()
        {
            listView2.Items.Clear();
            foreach (var item in user_List.users)
            {
                foreach (var i in item.Record)
                {
                    ListViewItem lvi = new ListViewItem(item.Username);
                    lvi.SubItems.Add(i.ToString());
                    listView2.Items.Add(lvi);
                }
            }
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
            if(checkBox1.Checked && checkBox2.Checked)
            {
                my_score();
                all_scores();
            }
            else if(checkBox1.Checked && !checkBox2.Checked)
            {
                my_score();
            }
            else if(!checkBox1.Checked && checkBox2.Checked) 
            {
                all_scores();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
        }


        void sendEmail()
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                "andrewmart1545@gmail.com",
                "chkojvgmrlorkvml");

            var message = new System.Net.Mail.MailMessage()
            {
                
                Subject = "Test Results",
                Body = $"\nHello {user.Username}, \nYour score: {user.Score}"
                    
            };

            message.From = new System.Net.Mail.MailAddress("andrewmart1545@gmail.com", "TEST CENTER");
            message.To.Add(new System.Net.Mail.MailAddress(user.Email));

            client.Send(message);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(sendEmail);
            thread.Start();
            MessageBox.Show("Score has been send");
        }

   
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            pictureBox29.Visible = false;
            pictureBox33.Visible = false;
            pictureBox34.Visible = false;
            pictureBox35.Visible = false;
            pictureBox36.Visible = true;
            textBox13.Visible = false;
            panel7.Show();
            panel6.Hide();
            panel4.Hide();
            panel3.Hide();
            panel7.Width = 620;
            panel7.Height = 538;
            textBox10.Text = user.Username;
            textBox11.Text = user.Password;
            textBox12.Text = user.Email;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox12.Text != user.Email)
                {
                    Thread thread = new Thread(SMTPrNew);
                    thread.Start();
                    MessageBox.Show("code has been sent");
                    textBox13.Visible = true;
                    pictureBox29.Visible = true;
                }
                else if (textBox12.Text == user.Email)
                {
                    MessageBox.Show("SUCSESSFULY DONE");
                    foreach (var user1 in user_List.users)
                    {
                        if (user1.Username == user.Username && user1.Password == user.Password)
                        {
                            user1.Username = textBox10.Text;
                            user1.Password = textBox11.Text;
                            user1.Score = user.Score;
                            user1.Email = textBox12.Text;
                            user1.Record = user.Record;
                            user.Username = user1.Username;
                            user.Password = user1.Password;
                            user.Email = user1.Email;
                        }
                    }
                    SaveUser();
                    
                    textBox10.Text = null;
                    textBox11.Text = null;
                    textBox12.Text = null;
                    label6.Text = user.Username;
                }
            }
            catch (Exception ex) { }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

            if (textBox13.Text == lis.First().ToString())
            {
                MessageBox.Show("SUCSESSFULY UPDATED");
                foreach (var user1 in user_List.users)
                {
                    if (user1.Username == user.Username && user1.Password == user.Password)
                    {
                        user1.Username = textBox10.Text;
                        user1.Password = textBox11.Text;
                        user1.Score = user.Score;
                        user1.Email = textBox12.Text;
                        user1.Record = user.Record;
                      
                        user.Username = user1.Username;
                        user.Password = user1.Password;
                        user.Email = user1.Email;
                    }
                }
                SaveUser();
               
                textBox10.Text = null;
                textBox11.Text = null;
                textBox12.Text = null;
            }
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            pictureBox29.Visible = false;

            textBox13.Visible = false;
            textBox13.Text = null;
            textBox10.Text = user.Username;
            textBox11.Text = user.Password;
            textBox12.Text = user.Email;
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel6.Hide();
            panel4.Hide();
            panel3.Hide();
            panel7.Hide();
            pictureBox21.Visible = true;
            pictureBox23.Visible = true;
            pictureBox22.Visible = true;
            label18.Visible = true;
            label19.Visible = true;
            LOGIN_MENU.Show();
            this.Height = 585;
            this.Width = 915;
            user.Username = "";
            user.Score = 0;
            user.Email = "";
            user.Record = null;
            listView1.Items.Clear();
            listView2.Items.Clear();
            pictureBox33.Visible = false;
            pictureBox34.Visible = false;
            pictureBox35.Visible = false;
            pictureBox36.Visible = false;

        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked) { groupBox3.Visible= true; }
            else if (!checkBox3.Checked) { groupBox3.Visible= false; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = 0;
            foreach (var item in questions.questions_History)
            {
                if (item.Name_Theory == comboBox1.SelectedItem.ToString())
                {
                    a++;
                }

            }
            label23.Text = "("+a.ToString();
        }

    
        private void A_q_Click_1(object sender, EventArgs e)
        {
            radioButton1.Select();
        }

        private void B_q_Click(object sender, EventArgs e)
        {
            radioButton2.Select();
        }

        private void C_q_Click(object sender, EventArgs e)
        {
            radioButton3.Select();
        }

        private void D_q_Click(object sender, EventArgs e)
        {
            radioButton4.Select();
        }
    }
}