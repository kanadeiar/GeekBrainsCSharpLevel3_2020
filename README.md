# GeekBrainsCSharpLevel3
Курс GeekBrains "C# Уровень 3"

## Методичка 1. C# Уровень 3. Введение в WPF. Архитектура приложения на C#

1. Посмотрите внимательно на приложение, при помощи которого мы тестировали возможность отправлять электронные письма и начали изучать WPF. Посмотрите внимательно на код. В коде есть несколько моментов, которые простительны для теста, но непростительны для серьёзного приложения.

a. Первый момент: жестко заданные переменные в коде. Например, new SmtpClient("smtp.yandex.ru", 25), в этой строке две жестко заданные переменные: "smtp.yandex.ru" – smtp-сервер и 25 – порт для этого сервера. В коде много и других жестко заданных переменных: адреса почтовых ящиков, тексты писем, тексты ошибок и др.
Задание: добавить в проект WpfTestMailSender public static class, без конструктора и методов. Определить в этом классе статические переменные и задать им значения. В коде использовать эти переменные вместо жестко заданных.

b. Второй момент, который также простителен для тестовой программы и нежелателен для более или менее серьезного приложения. Код, который описывает форму, и код, который занимается непосредственно рассылкой, содержится в одном классе.
Задание: добавить к проекту WpfTestMailSender public class, назвать его, например, EmailSendServiceClass с конструктором. Создать в этом классе методы (или метод), которые будут заниматься рассылкой писем. Класс надо создать таким образом, чтобы его можно было легко перенести в другой проект.

c. В коде присутствует MessageBox с выводом ошибки в случае невозможности отправить письма. В принципе, MessabeBox — не криминал и даже в серьезных проектах они присутствуют, но всё-таки окно со своим стилем выглядит лучше.
Задание: по аналогии с формой, которая выводит сообщение «Работа завершена», создайте ещё одну для вывода текста ошибки. Цвет текста ошибки пусть будет красным. Также добавьте кнопку «ОК», которая будет закрывать форму.

3. Теперь задание на укрепление знаний технологии WPF.

a. Добавить на главное окно тестового проекта WpfTestMailSender, в любое место формы два контрола TextBox, одно для названия письма, второе для самого текста письма. И сделайте так, чтобы название письма и текст письма брались из этих контролов.

b. Скачать библиотеку стилей или тем (theme) с сайта http://wpfthemes.codeplex. Как описано в главе Изменение стиля приложения WPF, выберите любой стиль по своему усмотрению и замените стиль, сделайте так же, как было описано на уроке.

c. Поиграйте с контролами тестового приложения WpfTestMailSender, поменяйте их свойства, поразмещайте в разных местах окна. Поменяйте основную панель Grid на другие панели, которые мы рассматривали на уроке.

4. Заменить название основного окна и класса приложения MailSender, MainWindow на WpfMailSender, сделать по аналогии с тем, как мы меняли название главного окна у тестового приложения WpfTestMailSender на уроке.

## Методичка 2. C# Уровень 3. Введение в WPF. Часть 2

1. Мы создали ToolBarTray и поместили на нём один из элементов ToolBar, на который, в свою очередь, поместили лейбл с названием, комбобокс с выбором отправителя и три кнопки. Задание:
a. По аналогии с комбобоксом по выбору отправителя надо создать комбобокс с выбором smtp-сервера. На лейбле написать «Выбрать smtp-server».
b. Добавить ToolBar «Перейти в планировщик» без комбобокса с одной кнопкой clock.png.
c. Добавить ToolBar «Список адресатов» без комбобокса с тремя кнопками.
По нажатию на кнопку перейти в планировщик, сделайте, чтобы происходил переход на вкладку «Планировщик». К комбобоксу «Выбрать smtp-server» привяжите Dictionary по аналогии с комбобоксом «Выбрать отправителя». Создайте Dictionary в том же классе, где ключом будет smtp-сервер, а значением (с типом int) порт smtp сервера. Сделайте так, чтобы значения сервера и порта передавались в экземпляр класса, который отвечает за отправку почты.
2. При отправке писем сделать проверку, есть ли текст в элементе RichTextBox во вкладке «Редактор писем». Если он пуст, то на экран выводить окно «Письмо не заполнено» и открывать вкладку «Редактор писем».
3. Скачать и установить WPF Toolkit http://wpftoolkit.codeplex.com/releases/view/617027. Добавить вкладку на ToolBox “Wpf Toolkit controls”. Затем кликнуть правой кнопкой мыши по “Choose item”. В диалоге, который появился, выбрать кнопку “Browse”и выбрать dll с Toolkit.
4. Во вкладке «Планировщик», заменить TextBox для ввода времени на элемент TimePicker.
5. Добавить картинку Letter2.jpg к кнопке «Отправить сразу» во вкладке «Планировщик».

## Методичка 3. C# Уровень 3. Разработка WPF-приложений c использованием шаблона MVVM на примере MVVM Light Toolkit.

1. По аналогии с тем, как мы создавали DLL из класса, который шифрует пароли, создать DLL из класса EmailSendServiceClass, который занимается рассылкой писем.
2. Посмотреть на ToolBarTray. Некоторые панели ToolBar очень похожи между собой. Из них можно сделать контрол и добавлять на ToolBar. Сделайте контрол из Панели «Выбрать отправителя» и добавьте его и в качестве контрола «Выбрать smtp-server». У этого контрола должна быть возможность заменить текст у лейбла, должен функционировать комбобокс и все три кнопки.

## Методичка 4. C# Уровень 3. Валидация модели или проверка вводимых данных на корректность. Unit-тестирование.

1. Задание направлено на повторение предыдущих трех уроков. Мы добавили реализацию возможности отправлять серию писем в один день при помощи класса SchedulerClass. Необходимо добавить такую возможность на весь проект.
a. Необходимо убрать параметр dtSend из сигнатуры метода SendEmails класса SchedulerClass.
b. Добавить возможность добавить несколько вариантов времени отправления писем на одну дату и соответствующих текстов писем.
##### 1. Вместо поля tbTimePicker (или tbTimePicker, для тех, кто смог заменить текстовое поле на элемент TimePicker) нужно добавить элемент ListView и сверху кнопку «Добавить письмо», по клику на которую будет добавляться новый элемент в ListView.
##### 2. В качестве элемента в ListView добавлять самописный контрол. Для которого нужно создать свой проект ListViewItemScheduler. Посередине должно быть поле, куда записываются даты. Кнопка «-» удаляет элемент из списка. Кнопка с карандашиком вызывает окно с RichTextBox, в который можно записать текст письма.
c. При клике на кнопку «Отправить запланированно» нужно забирать информацию из ListView, передавать её в класс SchedulerClass в поле Dictionary<DateTime, string> и обрабатывать так, как мы прописали на уроке.
2. Добавить UnitTest для метода getPassword проекта CodePasswordDLL.
a. Методы с тестами добавить в проект CodePasswordDLL.Test класс CodePasswordTest по аналогии с тестами для метода getCodPassword.
b. Добавить в класс CodePasswordTest методы TestInitialize и TestCleanup.
c. Расширить метод Assert.AreEqual параметром с сообщением для output.
d. Добавить Debug.WriteLine внутри тестовых методов и посмотреть, как записываются текстовые сообщения
e. Добавить тестовые методы для этого же класса с использованием методов StringAssert.
3. Добавить тестовые методы для класса SchedulerClassTest с использованием тестовой коллекции sc.DatesEmailTexts и методов класса CollectionAssert.

## Методичка 5. C# Уровень 3. Многопоточное программирование.

1. Написать приложение, считающее в раздельных потоках:
a. факториал числа N, которое вводится с клавиатуру;
b. сумму целых чисел до N, которое также вводится с клавиатуры.
2. *Написать приложение, выполняющее парсинг CSV-файла произвольной структуры и сохраняющее его в обычный TXT-файл. Все операции проходят в потоках. CSV-файл заведомо имеет большой объём.

## Методичка 6. C# Уровень 3. Параллельное программирование и TPL. Асинхронное программирование (TAP).

1. Даны 2 двумерных матрицы. Размерность 100х100 каждая. Напишите приложение, производящее параллельное умножение матриц. Матрицы заполняются случайными целыми числами от 0 до10.
2. *В некой директории лежат файлы. По структуре они содержат 3 числа, разделенные пробелами. Первое число — целое, обозначает действие, 1 — умножение и 2 — деление, остальные два — числа с плавающей точкой. Написать многопоточное приложение, выполняющее вышеуказанные действия над числами и сохраняющее результат в файл result.dat. Количество файлов в директории заведомо много.

## Методичка 7. C# Уровень 3. Базы данных.

1. Реализовать в приложении «Рассыльщик» отчет, выводящий существующих адресатов.
2. *Реализовать в приложении «Рассыльщик» функциональность, позволяющую добавлять, удалять и редактировать адресатов посредством Entity Framework.
3. *Написать программу для продажи билетов в кино. Продажа выполняется следующим образом: кассир выбирает киносеанс и указывает количество покупаемых билетов. Информация о продажах сохраняется базу данных для последующего анализа. Для заказа сохраняется количество билетов и время продажи. Дополнительно может быть предусмотрено редактирования списка киносеансов. Для киносеанса заполняется время начала и название фильма.
4. *Есть CSV-файл с таким содержанием:
Иванов И.И., ivanov@mail.ru, +7(111) 123-45-67
Петров П.П.,petrov@mail.ru, +7(222) 123-45-67
Федоров Ф.Ф., fedorov@mail.ru, +7(333) 123-45-67
Записи представляют собой значения: ФИО, почта, телефон. Необходимо написать приложение, которое:
a. импортирует данный файл в базу данных;
b. позволяет редактировать данные.

## Методичка 8. C# Уровень 3. Рефлексия, позднее связывание и атрибуты.

1. Выполнить без использования среды разработки, используя только ручку и лист бумаги, задачу на написание консольного приложения нахождения факториала числа N.
2. Есть ли проблемы в следующем коде?
int i = 1;
object obj = i;
++i;
Console.WriteLine(i);
Console.WriteLine(obj);
Console.WriteLine((short)obj);
3. Есть таблица Users. Столбцы в ней — Id, Name. Написать SQL-запрос, который выведет имена пользователей, начинающиеся на 'A' и встречающиеся в таблице более одного раза, и их количество.
4. Каков результат вывода следующего кода?
private enum SomeEnum
{
    First = 4,
    Second,
    Third = 7
}
static void Main(string[] args)
{
    Console.WriteLine((int)SomeEnum.Second);
}
5. *Существует таблица (см. методичку к уроку, раздел ДЗ). Необходимо сформировать SQL-запрос, выбирающий данные (см. методичку к уроку, раздел ДЗ).
6. Попробовать выполнить задания, приведенные в дополнительных материалах. (см. методичку к уроку, раздел Дополнительные материалы).
