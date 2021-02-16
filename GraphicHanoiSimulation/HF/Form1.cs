using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HF
{
    public partial class Form1 : Form
    {
        Form1 form;
        public Form1()
        {
            InitializeComponent();
        }
        public stack a, b ,c; //

        /*
   Nombre del programa: HF
   Problema:Se debe crear un algoritmo capaz de resolver las torres de hanoi
   Programador: Andrés González
   Fecha de creación: 18 de febrero de 2019
   Fecha de modificación:  18 de febrero de 2019
   Herramientas: ciclos, condiciones, stacks.
   Fin en mente: Realizar un algoritmo que resuelva las torres de hanoi
   Lenguaje: C#
   Procesos pendientes: Ninguno, trabajo completo.
   Correo: androsogt@gmail.com
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            a = new stack(1, 0, this); //RING SPOT A
            b = new stack(2, 300, this); //RING SPOT B
            c = new stack(3, 600, this); //RING SPOT C 
            form = this; //actual form
        }
        public void h(int n, stack a, stack b, stack c)
        {
            Button bb = new Button();
            if (n == 1)
            {
               bb = a.pp(a);
               c.push(bb, c);
              
            }
            /**RECURSIVE ALGORITHM*/
            else
            {
                h(n-1, a, c, b);
                h(1, a, b, c);
                h(n-1, b, a, c);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int an = Convert.ToInt32(textBox1.Text);
            for(int i = 0; i < an; i++)
            {
                c.pp(c).Dispose(); //CLEAR SOME MEMORY (STACK OBJECTS)
            }
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int amount_Rings = Convert.ToInt32(textBox1.Text);
            

            if (amount_Rings < 1 )
            {
                MessageBox.Show("At least 1 ring is required");
            }
            if (amount_Rings > 10)

            {
                MessageBox.Show("No more than 10 rings are allowed ");
            }
            
            else
            {
                button2.Enabled = true;
                button1.Enabled = false; 
                a.fi(amount_Rings, form, a);
                h(a.num, a, b, c);
                button2.Enabled = true;

            }

        }

        public class stack
        {
            //CONSTRUCTOR FOR THE RINGS
            //THIS CLASS IS USE THE BUILD THE 3 SPOT RINGS
            public int num;
            public Button[] hh = new Button[9]; //RING STACK
            public Button b;
            public Button al;
            public Form1 forma;

            public stack(int n, int x, Form ff)
            {
                b = new Button(); 
                b.Visible = true;
                b.Location = new Point(50 + x, 350);
                b.Width = 200;
                b.Height = 7;
                b.Left = 50 + x;
                b.Parent = ff;


               al = new Button();
               al.Parent = ff;
               al.Width = 7;
               al.Height = 200;
               al.Location = new Point(50 + x + 100, 151);
               al.Left = 50 + x + 100;
               al.SendToBack();
               b.SendToBack();
               num = 0;
            }
            public void push(Button e, stack ringSpot)
            {
                ringSpot.num++;
                ringSpot.hh[ringSpot.num] = e;

                if (ringSpot.hh[ringSpot.num].Left + ringSpot.hh[ringSpot.num].Width / 2 < ringSpot.al.Left)
                {
                    while (ringSpot.hh[ringSpot.num].Left + ringSpot.hh[ringSpot.num].Width / 2 < ringSpot.al.Left)
                    {
                        ringSpot.hh[ringSpot.num].Left = ringSpot.hh[ringSpot.num].Left + 10;

                        Application.DoEvents();
                        Thread.Sleep(3);
                    }
                }


                if (ringSpot.hh[ringSpot.num].Left + ringSpot.hh[ringSpot.num].Width / 2 > ringSpot.al.Left)
                {
                    while (ringSpot.hh[ringSpot.num].Left + ringSpot.hh[ringSpot.num].Width / 2 > ringSpot.al.Left)
                    {
                        ringSpot.hh[ringSpot.num].Left = ringSpot.hh[ringSpot.num].Left - 10;
                        Application.DoEvents();
                        Thread.Sleep(1);
                    }
                }


                if (ringSpot.hh[ringSpot.num].Top <= ringSpot.b.Top - (ringSpot.num * 20) - 3)

                {
                    while (ringSpot.hh[ringSpot.num].Top <= ringSpot.b.Top - (ringSpot.num * 20) - 3)
                    {
                        ringSpot.hh[ringSpot.num].Top = ringSpot.hh[ringSpot.num].Top + 10;
                        Application.DoEvents();
                        Thread.Sleep(1);
                    }
                }

            }

            public Button pp(stack f)
            {
                while (f.hh[f.num].Top > 50)
                {
                    f.hh[f.num].Top = f.hh[f.num].Top - 10;
                    Application.DoEvents();
                    Thread.Sleep(1);
                }
                return f.hh[f.num--];
            }
            public void fi(int rings, Form F, stack spotA)
                /*
                 fi procedure is used to create the first n amounts
                of ring in the ring spot A*/
            {
                for (int i = 1; i <= rings; i++)
                {
                    spotA.hh[i] = new Button();
                    spotA.hh[i].Parent = F;
                    spotA.hh[i].Height = 20;
                    spotA.hh[i].Width = 200 - 20 * i;
                    spotA.hh[i].Location = new Point(50, 50);
                    spotA.hh[i].Left = 50;
                    spotA.hh[i].Top = 50;
                    spotA.hh[i].BringToFront();
                    push(spotA.hh[i], spotA);
                }
            }

            internal object pp()
            {
                throw new NotImplementedException();
            }
        }
       
    }
}
