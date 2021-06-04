# Wizualizacja pracy perceptronu.

Zadaniem prymitywnej sieci neuronowej (złożonej z jednego perceptronu) jest znalezienie prostej dzielącej zestaw punktów na 2 połowy.


- Input:
    - Lista 2xfloat (może fajnie byłoby gdyby ten zbiór miał parzystą liczbę elementów).
- Output:
    - parametr a1 w funkcji liniowej: y = a1*x + a0;
    - lub kąt funkcji liniowej czyli alfa dla: y = tg(alfa)*x + a0;

### Materiały:
- Na jednej z pierwszych instrukcji do zajęć jest zasada działania perceptronu. [Link](https://cez2.wi.pb.edu.pl/moodle/pluginfile.php/16668/mod_resource/content/2/Projekty-2019.pdf).

### Sugestie:
- Można znaleźć funkcję która prawidłowo dzieli zestaw na 2 części.

### UI:
- Powinien zawierać 2 okna:
    - Widok perceptronu
    - Widok punktów, oraz obecnie wyznaczonej prostej dzielącej zbiór punktów.
    - Widok wykresu 
- Powinien być przycisk do generowania puktów.
- Widok perceptronu pozwala także na ustawienie własnych punktów poprzez klikanie w okno.
- Listę wyświetlającą punkty można zrobić w ten sposób, że powiększenie zbioru testowego to tylko klikanie na kolejne elementy listy.

### Plan działania:
- Wrzuacam to na gita i was dodaję.
> Ja to robię
- Ogarnąć monogame i rozpocząć budować UI.
> Ewentualnie fajniej by było przerzucić się na WPF lub Unity ew Godot.
> Daniel.
- Ogarnąć jak przeprowadzić uczenie sieci.
> Maciek.