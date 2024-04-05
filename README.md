Реализована программа для имитационного моделирования динамики полового и возрастного состава населения России за последние 50 лет.  Пользователь с помощью диалогов выбора файла указывает пути к файлам с первоначальным возрастным составом населения и таблице смертности. Затем с помощью интерфейса программы назначает или редактирует следующие параметры:
- год начала моделирования (по умолчанию - 1970);
- год окончания моделирования (по умолчанию - 2021);
- начальная общая численность населения (по умолчанию - 130 млн. чел.).
По нажатию на кнопку старта должен начинаться процесс моделирования с заданными параметрами. По окончании процесса, программа должна выводить следующую информацию:
- график изменения общего населения по годам в виде spline chart;
- график изменения населения мужского пола по годам в виде spline chart;
- график изменения населения женского пола по годам в виде spline chart;
- возрастной состав населения мужского пола на конец моделирования для возрастных категорий 0-18, 19-45, 45-65 и 65-100 лет в виде bar chart;
- возрастной состав населения женского пола на конец моделирования для возрастных категорий 0-18, 19-44, 45-65 и 66-100 лет в виде bar chart.

///

Входные файлы:
Первоначальный возрастной состав населения обоих полов, с которого должен начинаться процесс моделирования, представлен в файле InitialAge.csv. Первый столбец указывает на возраст, второй на количество человек соответствующего возраста в расчете на 1000. 
В файле DeathRules.csv представлены правила вычисления вероятности умереть человека каждого пола для определенного возраста, взятые из таблицы смертности. В нем первые два столбца отвечают за возрастной интервал, а третий и четвертый столбец хранят вероятность смерти мужчин и женщин из этих интервалов соответственно за один год из указанного интервала.

///

Движок моделирования в цикле отсчитывает проходящие года, посылая события всем людям о прошедшем годе. Каждый человек реагирует на данное событие обработкой собственных правил существования. 
Основные правила и концепции:
- Один объект человека (класс Person) моделирует 1 тыс. чел.
- Объект класса Person, как минимум, хранит информацию о собственном поле, годе рождения и статусе жизни (жив/мертв). В случае смерти записываеться информация о годе смерти.
- Каждый объект человека каждый год принимает решение о продолжении собственной жизни на основании вероятности умереть для своего пола и возрастной категории из таблицы смертности.
- После смерти, человек больше не изменяет свое состояние и не генерирует новых событий.
- Если человек жив, женского пола и в возрасте 18-45 лет, то он с вероятностью 0.151 может сгенерировать событие о рождении ребенка.
- При рождении ребенка вероятность того, что он девочка - 0.55, и мальчик - 0.45.
- В начале моделирования женщин и мужчин должно быть поровну, а их возрастное распределение определяется файлом InitialAge.csv.

