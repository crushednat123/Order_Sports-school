   M S B A M L   `   `   `  ����G  CSportsSchool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:SportsSchool.PagesSportsSchool.Pages  5      !    SportsSchool.Pages.UserPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:SportsSchool.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          N� Title$  UserPage��5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��65 �6   .+�5   
   � $��0��6    5      �� 5      $��60 �6    �� 5      .+�5      � $��0��6   .+�5      �� $��0,10,0,0q�5       �� Padding$ 4q�5      $��Center=�5      $	��Top=�5      ���	#FFF0E8F8 �� CornerRadius$ 10��5!      ._�5"      �� $��Center=�5#      $��Center=�5$      $��24(�5%      $
��Bold"�5&      ���	#FF9864C5 $   MaterialDesignSubtitle2TextBlock#���   5(      $��Пользователи��5)      5*      5+      &   System.Windows.Controls.DataGrid  5-      -   5          Name$ 	dateUsers��5.      $��1��5/         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . #��[B 54         Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 57      
   Header$
 Фамилия��59      1  +System.Windows.Controls.DataGridBoundColumn   Binding 58      �� *	SurName+59        5;      $	 * 5<      $
 Имя��5>       5=      �� *Name+5>        5@      $	 * 5A      $
 Отчество��5C       5B      �� *
MiddleName+5C        5E      $	 * 5F      $
 Роль��5H       5G      �� *	Role.Name+5H        5J      $	 * 5K      $
 
Почта��5M       5L      �� *Mail+5M        5O      $	 * 5P      $
 
Логин��5R       5Q      �� *Login+5R        5S      $	 * 5T      $
 Пароль��5V       5U      �� *
Password+5V      5X      5Y      � 5\   
   $��2��6   .+�5]      �� -   5         ��Name$ 
btnAdUsers��5^      $	��200��5_      $��10q�5`      $
��Left=�5a      $��Добавить��5c       � FocusVisualStyle 5d      Z� �� 5f      -   5        $ 	btnDelete��5g      $	��200��5h      $��10q�5i      $��Right=�5j      $��Удалить��5l       $  MaterialDesignOutlinedDarkButton#���  5n       5m      Z� 5n      5o      5p      5q      