   M S B A M L   `   `   `  ����A  =Galois, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GaloisGalois  5   	       Galois.Table   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Galois     5   	   -   5        $��window��5	   	     � Title$  Table��5   	   $	��750��6   $	��600��6$   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��Stretch=�6   $��Stretch=�62   $��0��6N   $��0,0,0,10q�6[   .+�5      �� $*��$Кількість стовпців:��6   $
��Bold"�62   �� 5      -   5        $
��Cols��5      $��70��6F   $��20��6Q   $��12(�6]   $��Arial)�6k   .@�5      �� $��8��6   
 J� Uid$ 8��6+   $��25��63    �� 
IsSelected . 6?   �� 5      $��10��6   $ 10��6,   $��25��65   �� 5      $��16��6   $ 16��6,   $��25��65   5      o� 5      $��Center=�6   $��5,0,5,0q�6.   .@�5      m� $��Зберегти ...��6   .@�5      m� -   5        $$��Зберегти у буфер��5   5   m� 5      -   5        $"��Зберегти у файл��5   3   5      5      �� 5      $��Ентропія:��6   $
��Bold"�6(   �� 5      -   5        $��enthropy��5   ,   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��
gistoTable��5         CanUserAddRows .     CanUserResizeRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeColumns .  	   CanUserSortColumns	 .  
   
IsReadOnly
 .    AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6B     RowHeaderWidth$ 50Q�6R     ColumnHeaderHeight$ 25Q�6f     	RowHeight$ 20Q�6~  $��12(�6�  $��1��6�     RowStyle 5       �� 5!       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5"      ��  �� Property � �� �� Value$ 12(�61   �� 5#       � ��$ Arial)�63   �� 5$       � ��$
 Bold"�63   5%         ColumnHeaderStyle 5'      �� 5(      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5)      ��  � ��$ 12(�61   �� 5*       � ��$ Arial)�63   �� 5+       � ��$
 Bold"�63   5,      5.      5/      50      