using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicketClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void reservationButton_Click(object sender, EventArgs e)
        {
            try
            {
                int ticketToReserve = Int32.Parse(this.ticketNumber.Text);
                string name = this.clientName.Text;
                this.responseLabel.Text = ticketToReserve > 0 && ticketToReserve < 13 ? RequestHandler.HandleRequest(ticketToReserve, name) : "Ingrese un número válido de cita";
            } catch
            {
                this.responseLabel.Text = "Ingrese un número válido para reserva de cita";
            }
        }
    }
}
