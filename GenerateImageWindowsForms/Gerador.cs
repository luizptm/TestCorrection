using System;
using System.Windows.Forms;
using TestCorrection.Library;

namespace GenerateImageWindowsForms
{
    public partial class Gerador : Form
    {
        Business b = new Business();

        public Gerador()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblResultado.Visible = true;
            lblResultado.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            b.TesteImagem();

            lblResultado.Visible = true;
            lblResultado.Text = "Imagem gerada com sucesso";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b.GenerateCandidates();
            
            lblResultado.Visible = true;
            lblResultado.Text = "Candidatos gerados com sucesso";
        }

        private void buttonTesteThread_Click(object sender, EventArgs e)
        {
            b.TesteThread();

            lblResultado.Visible = true;
            lblResultado.Text = "Thread testado com sucesso";
        }
    }
}
