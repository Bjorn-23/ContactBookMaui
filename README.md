# Contact Book Project
The goal of this assignment was to create a contact book that could add, view, update and delete contacts.<br>
The contacts should be written to a file on disk, and should be retrieved upon starting the application.<br>

## Structure
The project was build with a shared class librarary that both the console app and the Maui application has access to.<br>
It implements interfaces, dependancy injection, a few SOLID principles, service pattern and MVVVM for the MAUI part.<br>

## Funtions
The goals have been met and a user can indeed add, view, update and delete contacts to and from a file on the disk.<br>
The location of this file is environment dependant but here are locations where they show up on my system (win 11):<br><br>
ConsoleApp<br>
C:\Users\{your username here}\AppData\Local\Contacts<br><br>
MauiApp<br>
C:\Users\{your username here}\AppData\Local\Packages\com.companyname.contactbookmaui_9zz4h110yvjzm\LocalCache\Local\Contacts.json<br><br>

(NB that contactbookmaui_9zz4h110yvjzm might be named differently on your system)<br>

The Console app and Maui app use the same logic (PContactServices and FileRepository) and calls it within their program.<br>
Therefore a user can add a common path to file on disk and the 2 programs can work upon the same list<br>
however simultaneous use of the list with both programs has not been tested and is no advisable.<br><br>

This is however not true for the Android part of the app which will not make use of a direct path to disk<br>
such as "C:\Users\{your username here}\AppData\Local\Contacts<br>
but rather creates its own list while the app is runnning.

## Summary
A fun task in new environment ( for me, as of NOV-23) with a steep learning curve<br>
It was highly rewardable in terms of challenge and has granted me a new skill set.<br>
<br>
/Björn
