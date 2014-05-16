using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RFID_Inventory
{
    class PDTClient
    {
        private TcpClient client;
        private Exception lastexcept;

        public PDTClient(String ipaddress, int port)
        {
            try
            {
                client = new TcpClient(ipaddress, port);
            }
            catch (SocketException se)
            {
                lastexcept = se;
            }
        }

        public Exception getLastExcept() { return lastexcept; }

        public bool upload(string filename)
        {
            bool succeed = false;
            if (client != null)
            {
                string appdatpath = @"\Application Data\Inventory\";
                StreamReader infile = new StreamReader(appdatpath + filename);
                string file = infile.ReadToEnd();
                try
                {
                    Stream s = client.GetStream();
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    sw.AutoFlush = true;
                    sw.WriteLine(filename);
                    sw.WriteLine(file);
                    //read ack from server
                    sr.ReadLine();
                    s.Close();
                    succeed = true;
                }
                catch (IOException io)
                {
                    
                    lastexcept = io;
                }
                finally
                {
                    client.Close();
                }
            }
            return succeed;
        }

    }
}