using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
//У меня Август в кадетском корпусе с дополетной подготовкой, советую беречь жопы
namespace Satellite.Classes
{
    public class TodoViewModel
    {
        private static TodoViewModel _todoViewModel = new TodoViewModel();
        private ObservableCollection<Todo> _allToDos = new ObservableCollection<Todo>();

        public ObservableCollection<Todo> AllTodos
        {
            get
            {
                return _todoViewModel._allToDos;
            }
        }

        public IEnumerable<Todo> GetTodos()
        {
            try
            {

                using (MySqlConnection connection = new MySqlConnection("Server=kramskov.tk;Database=ezhupa;User Id=debohih;Password=analny;SslMode=None"))
                {
                    connection.Open();
                    MySqlCommand getCommand = connection.CreateCommand();
                    getCommand.CommandText = "SELECT whatToDO FROM todo";
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _todoViewModel._allToDos.Add(new Todo(reader.GetString("whatToDO")));
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                ContentDialog deleteFileDialog = new ContentDialog()
                {
                    Title = "Работает, да вот чет данные подключения херовые :)",
                    PrimaryButtonText = "ОК"
                };
                deleteFileDialog.ShowAsync();
            }
            return _todoViewModel.AllTodos;
        }

        public bool InsertNewTodo(string what)
        {
            Todo newTodo = new Todo(what);
            // Insert to the collection and update DB    
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=kramskov.tk;Database=ezhupa;User Id=analny;Password=debohih;SslMode=None"))
                {
                    connection.Open();
                    MySqlCommand insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = "INSERT INTO todo(whatToDO)VALUES(@whatToDO)";
                    insertCommand.Parameters.AddWithValue("@whatToDO", newTodo.whatToDO);
                    insertCommand.ExecuteNonQuery();
                    _todoViewModel._allToDos.Add(newTodo);
                    return true;

                }
            }
            catch (MySqlException)
            {
                // Don't forget to handle it    
                return false;
            }

        }


        public TodoViewModel()
        { //фу гавноооооо
        }
    }
}
