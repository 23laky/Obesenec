   M S B A M L   `   `   `  ����H  Dtest obesenec, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:test_obesenectest_obesenec  5   	   (    "test_obesenec.ZadejPrezdivkuWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:test_obesenec     5   	     � Title$  Zadejte přezdívku��5   	   $	��450��6#   $	��400��60    � WindowStartupLocation$ CenterScreen=�6<    
  window#��[  5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $
��0.5* �6   �� 5      $��* �6   �� 5      $
��0.5* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��ProhralVyhralLabel��5   '   $��Prohrál jsi!��6     labelProhraljsi#��[ 6A   $��45(�6j   �� 5   
   -   5        $��hadaneSlovoByloLabel��5      $��1��6   $��Hádané slovo bylo:��69   #��[ 6V   �� 5   
   -   5        $��tajenkaLabel��5      $��2��6   $��28(�6M    	 label#��[ 6[   ��61   �� *	Tajenka+6[   �� 5   
   -   5        $��tvojeSkoreLabel��5      $��3��6   $��Tvoje skóre:��64   $��14(�6K   #��[ 6Y   �� 5   
   -   5        $��
skoreLabel��5      $��4��6   $��#��6/   $��28(�6;   #��[ 6I   �� 5   
   -   5        $��zadejPrezdivkuLabel��5      $��5��6   $��Zadej svoji přezdívku:��68   #��[ 6Y   �� 5   
   -   5         ��Name$ prezdivkaTextBox��5      $��6��6   $��10q�69    �� TextAlignment$ Center=�6E   $��20��6\   6h   �� 5   
   -   5         ��Name$ prezdivkaOkButton��5      $��7��6   $	��OK!��69    
 button#��[ 6G   6g   5      5      