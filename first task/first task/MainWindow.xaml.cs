using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace first_task
{
    public class text//создаем класс с название текст 
    {
        public string xtext { get; set; }
        public int idwords { get; set; }
        public int idglas { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static async Task<string> GetJson(string id)// создаем строку подключения, которая принимает id для поиска текста
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("TMG-Api-Key", "0J/RgNC40LLQtdGC0LjQutC4IQ==");//задаем ключ
                return await client.GetStringAsync($"https://tmgwebtest.azurewebsites.net/api/textstrings/{id}");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CalculateBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idstr = index_string.Text;
                List<text> texts = new List<text>();//создаем список образца нашего созданного класса
                char[] separators= new char[] { ',', '.', '!', '?', '&', '"', ':', ';', '-', '_',' ' };
                string[] iidstr = idstr.Split(separators, StringSplitOptions.RemoveEmptyEntries);// очищаем строку от лишних знаков , и создаем массив id ключей
                int idarray = 0;
                try
                {
                    for (int y = 0; y < iidstr.Length; ++y)//цикил для нескольких запрсов
                        if (iidstr[y] != null && Enumerable.Range(1, 20).Contains(int.Parse(iidstr[y])))//проверка корректности введенных данных
                        {
                            {
                                string idsrx = iidstr[idarray];//создаем переменную со значением в массиве с индексом равным idarray
                                var json = await GetJson(idsrx);
                                JObject result = JObject.Parse(json);//получаем файл в формате json
                                string texthere = result["text"].ToString();//переводим json файл в текст 
                                var trimtexthere = texthere.Trim(new char[] { ',', '.', '!', '?', '&', '"', ':', ';', '-', '_' });
                                string[] textArray = trimtexthere.Split(new char[] { ' ' });
                                var iidwords = textArray.Length;//считаем количество слов
                                int iidglas = Regex.Matches(texthere, @"[ауоыэяюёиеaeiouäöüyuoieaœœ̃ɑ̃øɛɛ̃ɑ̃ɔ̃ə]", RegexOptions.IgnoreCase).Count;//считаем количество гласных
                                texts.Add(new text() { xtext = texthere, idwords = iidwords, idglas = iidglas });
                                ++idarray;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неккоректный ввод");
                            return;
                        }
                }
                catch 
                {
                    MessageBox.Show("Неккоректный ввод");
                    return;
                }
                    
                grdata.ItemsSource = texts;//передаем данные в дата грид хранящиеся в нашем списке
            }
            catch (Exception x) 
            {
                MessageBox.Show(x.ToString());
            }
        }

    }
}
