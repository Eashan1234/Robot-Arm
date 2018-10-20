using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    public partial class Form1 : Form
    {
        SerialPort arduino;
        KinectSensor kinectSensor;
        BodyFrameReader bodyFrameReader;
        Body[] bodies;
        bool dataReceived = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            arduino = new SerialPort();
            arduino.PortName = "COM3";
            arduino.BaudRate = 9600;
            
            initializeKinect();
        }

        public void initializeKinect()
        {
            kinectSensor = KinectSensor.GetDefault();

            if (kinectSensor != null)
            {
                kinectSensor.Open();
            }

            bodyFrameReader = kinectSensor.BodyFrameSource.OpenReader();

            if(bodyFrameReader != null)
            {
                bodyFrameReader.FrameArrived += Reader_FrameArrived;
            }
        }

        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }


                if (dataReceived)
                {
                    foreach (Body body in bodies)
                    {
                        if (body.IsTracked)
                        {
                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                            Joint hand = joints[JointType.HandRight];

                            float x = hand.Position.X;
                            float y = hand.Position.Y;
                            float z = hand.Position.Z;

                            textBox5.Text = x.ToString("#.##");
                            textBox4.Text = y.ToString("#.##");
                            textBox3.Text = z.ToString("#.##");

                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
