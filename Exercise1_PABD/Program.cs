using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Exercise1_PABD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan Database tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk terhubung ke Database : ");
                    char cr = Convert.ToChar(Console.ReadLine());
                    switch (cr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source =  FADLISTEV\\FADLI036; " +
                                    "initial catalog = {0}; " +
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("\n Enter your Choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());

                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Sewa Mobil\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);

                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Admin\n");
                                                    Console.WriteLine("Masukkan Id_denda : ");
                                                    string id_denda = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Keterlambatan: ");
                                                    string Keterlambatan = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kerusakan (kecil/ sedang/ besar): ");
                                                    string Kerusakan = Console.ReadLine();
                                                   
                                                    try
                                                    {
                                                        pr.insert(id_denda, Keterlambatan, Kerusakan, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki" + "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid Option");
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered. ");
                                    }
                                }

                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid Option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From denda", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }

        public void insert(string id_denda,string Keterlambatan, string Kerusakan,  SqlConnection con)
        {
            string str = "";
            str = "insert into denda (id_denda, Keterlambatan, Kerusakan)" + "values (@id_denda, @Keterlambatan, @Kerusakan)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id_denda", id_denda));
            cmd.Parameters.Add(new SqlParameter("Keterlambatan", Keterlambatan));
            cmd.Parameters.Add(new SqlParameter("Kerusakan", Kerusakan));
           
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }

        
    }
}
