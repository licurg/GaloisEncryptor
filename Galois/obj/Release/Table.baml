   M S B A M L   `   `   `  ���� A  =Galois, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GaloisGalois      Galois.Table   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Galois     -   $��window��  � Title$  Table��$	��750��$	��600��.���  � RowDefinitions �� $
��Auto ��� $��* �.+�]� $��Stretch=�$��Stretch=�$��0��$��0,0,0,10q�.+��� $*��$Кількість стовпців:��$
��Bold"��� -   $
��Cols��$��70��$��20��$��12(�$��Arial)�.@��� $��8��
 J� Uid$ 8��$��25�� �� 
IsSelected . �� $��10��$ 10��$��25���� $��16��$ 16��$��25��o� $��Center=�$��5,0,5,0q�.@�m� $��Зберегти ...��.@�m� -   $$��Зберегти у буфер��m� -   $"��Зберегти у файл���� $��Ентропія:��$
��Bold"��� -   $��enthropy��&   System.Windows.Controls.DataGrid  -   $��
gistoTable��   CanUserAddRows .     CanUserResizeRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeColumns .  	   CanUserSortColumns	 .  
   
IsReadOnly
 .    AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    RowHeaderWidth$ 50Q�   ColumnHeaderHeight$ 25Q�   	RowHeight$ 20Q�$��12(�$��1��   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .���  �� Property � �� �� Value$ 12(���  � ��$ Arial)���  � ��$
 Bold"�   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$ 12(���  � ��$ Arial)���  � ��$
 Bold"�