# Requirements

## Background and User Requirements

* [Mark Forster - Square Space blog](http://markforster.squarespace.com/blog/2017/12/2/simple-scanning-the-rules.html) as feeder information on this project.
  * Long lists for task management.
  * Develop software for Simple Scanning.
    * Actions which can be completed on a list of things:
      * Cross-Out (Completed)
      * Re-Enter (cross-out, and moved to the bottom of the list)
      * Skip (Do later)
    * Console UI.
    * Single line items, user enters lines of text (freeform)
    * view by page:
    * 1st page beings with first unactioned item. (everything about this has been completed and deleted.)
    * pages containing no unactioned items are persevered and display
    * added items always become the last item on the "long list"
    * Pages of items should contain 20 items
    * Completed items should be notated by grey text.
    * Current item of work should be highlighted (invert foreground and background color)
    * Data stored in a flat text file, in the current directory
    * should be able to add a task at any time
    * Stretch item
    "item info" e.g. entry data or # of re-entries which displays separately from task list.

## Functional Requirements

* Upon execution of the of program at the command line, and the user has not exited the program, the program shall run in a console UI until the user presses a key combination which causes the program to exit.

* The user shall see single line items of up to 20 tasks which have been input by the user in free text format, while the program is running.

* The user shall be able to see the tasks on the task list over 20 items in length by pressing a key on the keyboard which then displays up the next 20 items on the task list.

* The user shall be able to mark individual items on the task list as completed which will then change the item's text color to grey.

* Tasks input by the user will always go to the end of task list.

* The task list shall be stored in a text file within the same directory from where the application executable is stored.

* The user shall be able to select an item as the current item of work, and when selected the item should be highlighted (invert foreground and background colors)
