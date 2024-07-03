using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Session1.DataSourse;

namespace Session1.Windows;

public partial class AddItem : Window
{
    private string SSelectedProductImageSource = Helper.DataObj.PSelectedProduct != null ? Helper.DataObj.PSelectedProduct.ImageSource : null; //Картинка попадаемого в это окно объекта сохраняется до выхода (чтобы восстановить ее если не сохранены данные при редактировании)
    private string? SSelectedImage = null; //выбранное изображение
    
    public AddItem()
    {
        InitializeComponent();
        panelHeader.Text = Helper.DataObj.PSelectedProduct == null ? "Создать товар" : "Редактирование товара"; //Содержание заголовка и кнопки сохранения изменений меняются в зависимости от содержания поля для выбранного продукта
        aConfirm.Content = Helper.DataObj.PSelectedProduct == null ? "Добавить" : "Сохранить";
        aCathegories.ItemsSource = Helper.DataObj.Cathegories; //выпадающему списку задается источник (статический список категорий)
        aCathegories.SelectedIndex = 0; //Выбранный элемент списка категорий - "Разное"
        aMeasurements.ItemsSource = Helper.DataObj.Measurem; //выпадающему списку задается источник (статический список единиц измерения)
        aMeasurements.SelectedIndex = 0; //Выбранный элемент списка единиц измерения - "шт."
        ProductCheck(); //проверка на наличие выбранного товара
    }

    private void ProductCheck() //Проверка наличия выбранного товара
    {
        if (Helper.DataObj.PSelectedProduct != null) //Если товар редактируется, то поля с редактируемыми данными заполняются автоматически
        {
            aId.Text = $"ID: {Helper.DataObj.PSelectedProduct.Idd}";
            aName.Text = Helper.DataObj.PSelectedProduct.Name;
            aSupplier.Text = Helper.DataObj.PSelectedProduct.Supplier;
            aPrice.Text = Convert.ToString(Helper.DataObj.PSelectedProduct.Price);
            aCount.Text = Convert.ToString(Helper.DataObj.PSelectedProduct.Count);
            aDescription.Text = Helper.DataObj.PSelectedProduct.Description;
            aCathegories.SelectedIndex = Helper.DataObj.PSelectedProduct.Cathegory;
            aMeasurements.SelectedIndex = Helper.DataObj.PSelectedProduct.Measurement;
            if (Helper.DataObj.PSelectedProduct.ImageSource != null) //Если у товара есть изображение, то отображается его превью
            {
                preview.IsVisible = imgPreview.IsVisible = true; //Изображение и название картинки становятся видимыми
                preview.Text = SSelectedImage = Helper.DataObj.PSelectedProduct.ImageSource; //тексту превью и полю для выбранного изображения передается название файла изображения
                imgPreview.Source = new Bitmap($"Assets/{Helper.DataObj.PSelectedProduct.ImageSource}"); //картинке превью передается битмап изображение с названием файла изображения из ассетов в качестве источника
            }
        }
    }

    private void newListWindow() //переход в окно со списком товаров
    {
        Helper.DataObj.PSelectedProduct = null; //Очистка полей редактируемого товара и выбранного изображения
        Authorized Authorized = new Authorized();
        Authorized.Show();
        Close();
    }

    private void AddActivity(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            var button = (sender as Button)!;
            switch (button.Name)
            {
                case "aCancel": 
                    if (SSelectedImage != null && SSelectedImage != SSelectedProductImageSource) 
                        File.Delete($"Assets/{SSelectedImage}"); //Файл изображения удаляется из папки ассетов
                    newListWindow();
                    break;
                case "aConfirm": //Подтверждение
                    if (Helper.DataObj.PSelectedProduct != null) //Если поле редактируемого товара не пустое (т.е. выбранный товар редактируется)
                    {
                        for (int i = 0; i < Helper.DataObj.Products.Count; i++)//поиск редактируемого товара в списке товаров
                        {
                            if (Helper.DataObj.Products[i].Idd == Helper.DataObj.PSelectedProduct.Idd) //если найден:
                            {
                                if (SSelectedImage != SSelectedProductImageSource && SSelectedProductImageSource != null) //Если было установлено новое изображение, 
                                    File.Delete($"Assets/{SSelectedProductImageSource}");

                                Helper.DataObj.Products[i] = new Product(aName.Text, Convert.ToDouble(aPrice.Text), Helper.DataObj.PSelectedProduct.Idd, Convert.ToInt32(aCount.Text), aSupplier.Text,aMeasurements.SelectedIndex,aDescription.Text, SSelectedImage,  aCathegories.SelectedIndex); //замена товара в списке товаров на новый с обновленными данными
                                newListWindow();
                                break;
                            }
                        }
                    }
                    else //Если поле редактируемого товара пустое (т.е. создается новый товар)
                    {
                        Helper.DataObj.Products.Add(new Product(aName.Text, Convert.ToDouble(aPrice.Text), Helper.DataObj.Products.Count, Convert.ToInt32(aCount.Text), aSupplier.Text,aMeasurements.SelectedIndex,aDescription.Text,SSelectedImage, aCathegories.SelectedIndex)); //Добавляется новый товар в конец списка товаров
                        newListWindow();
                    }
                    break;
            }
        }
        catch
        {
            if (SSelectedImage != null && SSelectedImage != SSelectedProductImageSource) //если до исключения было выбрано новоне изображение, оно удалится из ассетов. Если у товра уже было изображение, оно останется на месте
                File.Delete($"Assets/{SSelectedImage}");
            newListWindow();
        }
    }

    private readonly FileDialogFilter fileFilter = new() //Фильтр для проводника
    {
        Extensions = new List<string>() { "png", "jpg", "jpeg" }, //доступные расширения, отображаемые в проводнике
        Name = "Файлы изображений" //пояснение
    };

    private string SameName(string filename) //Проверка уникальности имени файла
    {
        string[] withExtentions = Directory.GetFiles("Assets"); //Получение названий всех изображений из ассетов с расширениями
        List<string> withoutExtentions = []; //инициализация нового списка для названий файлов без расширений

        foreach (string file in withExtentions)
            withoutExtentions.Add(Path.GetFileNameWithoutExtension(file)); //В новый список передаются названия файлов без расширений

        foreach (string file1 in withoutExtentions) //перебор каждого названия файла из списка названий
            if (file1 == filename) //если название одного из файлов идентично названию файла заданного в методе
            {
                return filename; //возвращает название файла
            }
        return null; //если такой файл не был найден, возвращает null
    }

    private async void ImageSelection(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var button = (sender as Button)!;
        switch (button.Name){
            case "imgAdd": 
                OpenFileDialog dialog = new(); //Открытие проводника
                dialog.Filters.Add(fileFilter); //Применение фильтра
                string[] result = await dialog.ShowAsync(this); //Выбор файла
                if (result == null || result.Length == 0)
                    return;//Если закрыть проводник то картинка не будет выбрана

                string imageName = Path.GetFileName(result[0]); //получение имени файла
                string[] extention = imageName.Split('.'); //Название файла делится на название и расширение
                string temp = extention[0]; //В изменяемой переменной хранится название файла. Оно будет меняться в процессе
                int i = 1; //Счетчик
                while(SameName(temp) != null) //Пока метод для проверки уникальности файла возвращает название файла
                {
                    temp = extention[0] + $"{i}"; //Новое имя файла
                    i++;
                }
                imageName = temp + '.' + extention[1]; //Новое имя файла с расширением

                File.Copy(result[0], $"Assets/{imageName}", true); //Копирование файла в папку ассетов

                if (SSelectedImage != null && SSelectedImage != SSelectedProductImageSource) //Если до установки новой картинки была выбрана другая, и при этом выбранная картинка не значение из поля, хранящее изначальноне изображение товара
                    File.Delete($"Assets/{SSelectedImage}"); //Удаление предыдущего изображения из ассетов

                preview.IsVisible = imgPreview.IsVisible = true; //Установление видимости 
                preview.Text = SSelectedImage = imageName;
                imgPreview.Source = new Bitmap($"Assets/{imageName}");

                break;
            case "imgDel":
                preview.IsVisible = imgPreview.IsVisible = false; //Превью становится невидимым
                SSelectedImage = null;//очистка поля с выбранным изображением

                if (preview.Text != SSelectedProductImageSource) //Удаление произойдет только если удаляемое изображение не является значением из поля, хранящее изначальноне изображение товара
                    File.Delete($"Assets/{preview.Text}"); //Удаление файла по названию из превью-текстблока

                break;
        }
    }
}