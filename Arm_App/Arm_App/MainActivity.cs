using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Java.Util;
using Android.Bluetooth;
using System.Threading.Tasks;

namespace Arm_App
{
    [Activity(Label = "Arm_App", MainLauncher = true, Icon = "@drawable/iconoarm")]
    public class MainActivity : Activity
    {

        ToggleButton conectar;
        TextView Result;
        TextView Result2;
        TextView Result3;
        TextView Result4;
        TextView Result5;
        TextView Result6;
        int progres1;
        int progres2;
        int progres3;
        int progres4;
        int progres5;
        int progres6;


        //String a enviar
        private Java.Lang.String dataToSend;
        //Variables para el manejo del bluetooth Adaptador y Socket
        private BluetoothAdapter mBluetoothAdapter = null;
        private BluetoothSocket btSocket = null;
        //Streams de lectura I/O
        private Stream outStream = null;
        private Stream inStream = null;
        //MAC Address del dispositivo Bluetooth
        private static string address = "00:15:83:35:6B:9B";
        //Id Unico de comunicacion
        private static UUID MY_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            conectar = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            Result = FindViewById<TextView>(Resource.Id.textView1);
            Result2 = FindViewById<TextView>(Resource.Id.textView2);
            Result3 = FindViewById<TextView>(Resource.Id.textView3);
            Result4 = FindViewById<TextView>(Resource.Id.textView4);
            Result5 = FindViewById<TextView>(Resource.Id.textView5);
            Result6 = FindViewById<TextView>(Resource.Id.textView6);


            var seekbar1 = FindViewById<SeekBar>(Resource.Id.seekBar1);
            var seekbar2 = FindViewById<SeekBar>(Resource.Id.seekBar2);
            var seekbar3 = FindViewById<SeekBar>(Resource.Id.seekBar3);
            var seekbar4 = FindViewById<SeekBar>(Resource.Id.seekBar4);
            var seekbar5 = FindViewById<SeekBar>(Resource.Id.seekBar5);
            var seekbar6 = FindViewById<SeekBar>(Resource.Id.seekBar6);
            conectar.CheckedChange += tgConnect_HandleCheckedChange;

             CheckBt();

            seekbar1.StopTrackingTouch += (s, e) =>
            {

                dataToSend = new Java.Lang.String("a" + progres1 + "\n");
                writeData(dataToSend);
            };

            seekbar1.ProgressChanged += (s, e) =>
            {
                progres1 = e.Progress;
                Result.Text = string.Format("1DOF: {0}", e.Progress);

            };

            seekbar2.StopTrackingTouch += (s, e) =>
            {

                dataToSend = new Java.Lang.String("b" + progres2 + "\n");
                writeData(dataToSend);
            };

            seekbar2.ProgressChanged += (s, e) =>
            {
                progres2 = e.Progress;
                Result2.Text = string.Format("2DOF: {0}", e.Progress);

            };
            seekbar3.StopTrackingTouch += (s, e) =>
            {

                dataToSend = new Java.Lang.String("c" + progres3 + "\n");
                writeData(dataToSend);
            };

            seekbar3.ProgressChanged += (s, e) =>
            {
                progres3 = e.Progress;
                Result3.Text = string.Format("3DOF: {0}", e.Progress);

            };
            seekbar4.StopTrackingTouch += (s, e) =>
            {

                dataToSend = new Java.Lang.String("d" + progres4 + "\n");
                writeData(dataToSend);
            };

            seekbar4.ProgressChanged += (s, e) =>
            {
                progres4 = e.Progress;
                Result4.Text = string.Format("4DOF: {0}", e.Progress);

            };
            seekbar5.StopTrackingTouch += (s, e) =>
            {

                dataToSend = new Java.Lang.String("e" + progres5 + "\n");
                writeData(dataToSend);
            };

            seekbar5.ProgressChanged += (s, e) =>
            {
                progres5 = e.Progress;
                Result5.Text = string.Format("5DOF: {0}", e.Progress);

            };
            seekbar6.StopTrackingTouch += (s, e) =>
            {

                dataToSend = new Java.Lang.String("f" + progres6 + "\n");
                writeData(dataToSend);
            };

            seekbar6.ProgressChanged += (s, e) =>
            {
                progres6 = e.Progress;
                Result6.Text = string.Format("6DOF: {0}", e.Progress);

            };


        }






        //Metodo de verificacion del sensor Bluetooth
        private void CheckBt()
        {
            //asignamos el sensor bluetooth con el que vamos a trabajar
            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            //Verificamos que este habilitado
            if (!mBluetoothAdapter.Enable())
            {
                Toast.MakeText(this, "Bluetooth Desactivado",
                    ToastLength.Short).Show();
            }
            //verificamos que no sea nulo el sensor
            if (mBluetoothAdapter == null)
            {
                Toast.MakeText(this,
                    "Bluetooth No Existe o esta Ocupado", ToastLength.Short)
                    .Show();
            }
        }
        //Evento de cambio de estado del toggle button
        void tgConnect_HandleCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                //si se activa el toggle button se incial el metodo de conexion
                Connect();
            }
            else
            {
                //en caso de desactivar el toggle button se desconecta del arduino
                if (btSocket.IsConnected)
                {
                    try
                    {
                        btSocket.Close();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        //Evento de conexion al Bluetooth
        public void Connect()
        {
            //Iniciamos la conexion con el arduino
            BluetoothDevice device = mBluetoothAdapter.GetRemoteDevice(address);
            System.Console.WriteLine("Conexion en curso" + device);

            //Indicamos al adaptador que ya no sea visible
            mBluetoothAdapter.CancelDiscovery();
            try
            {
                //Inicamos el socket de comunicacion con el arduino
                btSocket = device.CreateRfcommSocketToServiceRecord(MY_UUID);
                //Conectamos el socket
                btSocket.Connect();
                System.Console.WriteLine("Conexion Correcta");
            }
            catch (System.Exception e)
            {
                //en caso de generarnos error cerramos el socket
                Console.WriteLine(e.Message);
                try
                {
                    btSocket.Close();
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Imposible Conectar");
                }
                System.Console.WriteLine("Socket Creado");
            }
            //Una vez conectados al bluetooth mandamos llamar el metodo que generara el hilo
            //que recibira los datos del arduino
            //beginListenForData();
            //NOTA envio la letra e ya que el sketch esta configurado para funcionar cuando
            //recibe esta letra.
          //  dataToSend = new Java.Lang.String("e");
            //writeData(dataToSend);
        }
        //Evento para inicializar el hilo que escuchara las peticiones del bluetooth
        public void beginListenForData()
        {
            //Extraemos el stream de entrada
            try
            {
                inStream = btSocket.InputStream;
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Creamos un hilo que estara corriendo en background el cual verificara si hay algun dato
            //por parte del arduino
            Task.Factory.StartNew(() => {
                //declaramos el buffer donde guardaremos la lectura
                byte[] buffer = new byte[1024];
                //declaramos el numero de bytes recibidos
                int bytes;
                while (true)
                {
                    try
                    {
                        //leemos el buffer de entrada y asignamos la cantidad de bytes entrantes
                        bytes = inStream.Read(buffer, 0, buffer.Length);
                        //Verificamos que los bytes contengan informacion
                        if (bytes > 0)
                        {
                            //Corremos en la interfaz principal
                            RunOnUiThread(() => {
                                //Convertimos el valor de la informacion llegada a string
                                string valor = System.Text.Encoding.ASCII.GetString(buffer);
                                //Agregamos a nuestro label la informacion llegada
                                Result.Text = Result.Text + "\n" + valor;
                            });
                        }
                    }
                    catch (Java.IO.IOException)
                    {
                        //En caso de error limpiamos nuestra label y cortamos el hilo de comunicacion
                        RunOnUiThread(() => {
                            Result.Text = string.Empty;
                        });
                        break;
                    }
                }
            });
        }
        //Metodo de envio de datos la bluetooth
        private void writeData(Java.Lang.String data)
        {
            //Extraemos el stream de salida
            try
            {
                outStream = btSocket.OutputStream;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error al enviar" + e.Message);
            }

            //creamos el string que enviaremos
            Java.Lang.String message = data;

            //lo convertimos en bytes
            byte[] msgBuffer = message.GetBytes();

            try
            {
                //Escribimos en el buffer el arreglo que acabamos de generar
                outStream.Write(msgBuffer, 0, msgBuffer.Length);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error al enviar" + e.Message);
            }
        }

    }
}

