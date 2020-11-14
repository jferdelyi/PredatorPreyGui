using System;
using System.Windows.Forms;

namespace PredatorPreyFormGui {
    public partial class PredatorPreyForm : Form {
        public PredatorPreyForm() {
            InitializeComponent();
        }

        public void UpdateLabels() {
            label1.Text = "Step: " + predatorPreyGui1.Environment.Step;
            label2.Text = "Number of agents: " + predatorPreyGui1.Environment.NoAgents;
            label3.Text = "Number of ants: " + predatorPreyGui1.Environment.NoAnts;
            label4.Text = "Number of doodlebugs: " + predatorPreyGui1.Environment.NoDoodlebugs;
            label5.Text = "Number of empty cells: " + predatorPreyGui1.Environment.NoEmpty;
            label1.Refresh();
            label2.Refresh();
            label3.Refresh();
            label4.Refresh();
            label5.Refresh();
        }


        private void Form1_Load(Object sender, EventArgs e) {
            predatorPreyGui1.Form = this;
        }

        private void splitContainer1_Panel1_Paint(Object sender, PaintEventArgs e) {

        }

        private void button1_Click(Object sender, EventArgs e) {
            predatorPreyGui1.Environment.Running = !predatorPreyGui1.Environment.Running;
        }

        private void label1_Click(Object sender, EventArgs e) {

        }

        private void label2_Click(Object sender, EventArgs e) {

        }

        private void splitContainer1_Panel2_Paint(Object sender, PaintEventArgs e) {

        }
    }
}
