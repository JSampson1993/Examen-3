
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Examen_3.Models;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace Examen_3.ViewModels
{
     public class BaseViewModel : INotifyPropertyChanged
    {
        ArticulosMVVM articulosMVVM = new ArticulosMVVM();
        public event PropertyChangedEventHandler PropertyChanged;

        private int id_pago;
        public int Id_Pago
        {
            get { return id_pago; }
            set
            {
                id_pago = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id_Pago"));
            }
        }

        private double monto;
        public double Monto
        {
            get { return monto; }
            set
            {
                monto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Monto"));
            }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Descripcion"));
            }
        }

        private byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Photo"));
            }
        }

        private DateTime dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DueDate"));
            }
        }

        private List<Pagos> taskList;
        public List<Pagos> TaskList
        {
            get { return taskList; }
            set
            {

                taskList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
            }
        }
        public Command cmdAddTask { get; set; }
        public Command cmdAddTask1 { get; set; }
        public Command cmdeliminar { get; set; }
        public BaseViewModel()
        {
            cmdAddTask = new Command(AddTask);
          //  cmdeliminar = new Command(OnDelete);
            getTask();
        }

        public ICommand UpdateCommand
        {
            get
            {
                return new RelayCommand(update);
            }
        }

        private async void AddTask(object obj)
        {
            var r = await App.BaseDatos.SaveTaskAsync(new Pagos
            {
                Descripcion = descripcion,
                Monto = Monto,
                Fecha = dueDate,
                ///Photo_recibo=Photo
            });

            getTask();

            await Application.Current.MainPage.DisplayAlert("Alert", "Informacion Guardada", "OK");
        }

        
        public async void update()
        {
            if (!string.IsNullOrEmpty(Id_Pago.ToString()))
            {
                Pagos person = new Pagos()
                {
                    Id_pago = Convert.ToInt32(Id_Pago.ToString()),
                    Descripcion = descripcion.ToString(),
                    Monto = Convert.ToInt32(monto.ToString())

                };

                //Update Person  
                await App.BaseDatos.SaveTaskAsync(person);

                await Application.Current.MainPage.DisplayAlert("Success", "Person Updated Successfully", "OK");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }
    
        private async void deleteAsync1()
        {
            if (!string.IsNullOrEmpty(id_pago.ToString()))
            {
                //Get Person

                var person = await App.BaseDatos.GetItemAsync(Convert.ToInt32(id_pago.ToString()));
                if (person != null)
                {
                    //Delete Person
                    await App.BaseDatos.deleteAsync(person);
                    await Application.Current.MainPage.DisplayAlert("Success", "Person Deleted", "OK");

                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }

        public async void getTask()
        {
            TaskList = await App.BaseDatos.GetTaskAsync();
        }

        //private async void OnDelete(int id) {

        //    Pagos pad

        //    if (pag== null)
           
        //    await App.BaseDatos.DeleteItemAsync(pag.Id_pago);


        //}
    }
}
