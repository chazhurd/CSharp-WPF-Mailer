   M S B A M L   `   `   `  ����M  IFinalHolidayMailer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:FinalHolidayMailerFinalHolidayMailer  5   	   #    FinalHolidayMailer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:FinalHolidayMailer     5   	     � Title$  
MainWindow��5   	   $	��366��6   $��467.667��6)    � 
ResizeMode$ NoResize=�69   .��5	      � $��0,0,-5.333,0q�6   $	��338��6!   $��Bottom=�6.    � ColumnDefinitions 5
   
   �� 5      �� 5      $��0* �6   �� 5      $��0* �6   �� 5      $��0* �6   ��5   
   �� 5       �� ImageSource$ Holiday_Party_BANNER.jpg��6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ GridDatabse��5   /      AutoGenerateColumns .     CanUserAddRows . $��47,130,24,2q�6~   
���  �� J� OpacityMask
 �  ��$��Vladimir Script)�6�   $��20(�6�   ��6Z   �� *TestBinding+��5      �� 5       �� Color$ 	#FFF0F0F0��6"   	 �� Opacity$		 0.4Q�64   
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ First��6%      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6`   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *First+6`     5      $
 Last��6%    . $ 80 6`    6G   �� *Last+6`     5      $ Email��6%    . $	 160 6`    6F   �� *Email+6`     5      $ 	Received?��6&    . $ 80 6i    6L   �� *
Received+6i   5      �� 5   
   -   5         ��Name$ 	txtbxFind��5      $
��Left=�6%   $��23��6@   $��
149,10,0,0q�6L    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��215��6�   ���	#FF2D2D30$��Enter Last Name Here��6�   ���	#FF93A6D9.��6  ��5      �� 5       $ 	#FF007ACC��6"   $		 0.9Q�64   .��5!   "   5"      �� 5#   
   -   5         ��Name$ lblEnterLast��5#      $��Search By Surname:��6&   $
��Left=�6C   $��	43,10,0,0q�6^   $	��Top=�6q   $	��101��6�   $��23��6�   
�������$��8(�6�   $
��Bold"�6�   ��5$      �� 5%      $ White��6"   $	 0.835Q�60   5'      c� 5(   
   $��44��6   $��
43,75,24,0q�6   $	��Top=�62   .@�5)      �� -   5         ��Name$ btnAddPerson��5)      $�� ��6+   $
��Left=�66   $��Stretch=�6Q   $��37��6m    J� RenderTransformOrigin$ -0.012,0.952&�6�   $��0,0,0,2.333q�6�   ��6�   Z� ��5*      �� 5+      $! add-person-2646097_1280.png��6!    j� Stretch$ Uniform=�6K   5-      �� 5.      -   5        $ btnEmailAll��5.      $�� ��6*   $
��Left=�65   $��Stretch=�6P   $��40��6l   $��0,0,0,-0.667q�6�   ��5/      �� 50      $ emailAll.png��6!   $ UniformToFill=�6<   52      �� 53      -   5        $ 
btnRecOnly��53      $�� ��6)   $
��Left=�64   $	��Top=�6O   $��40��6g   $��40��6r   ��54      �� 55      $ emailRec.png��6!   57      �� 58      -   5        $ btnReset��58      $�� ��6'   $
��Left=�62   $	��Top=�6M   $��40��6e   $��40��6�   ��59      �� 5:      $ 415758-200.png��6!   $ Uniform=�6>   5<      �� 5=      -   5        $ btnExit��5=      $�� ��6&   $
��Left=�61   $	��Top=�6L   $��40��6d   $��40��6o   ��5>      �� 5?      $ Exit.png��6!   $ Uniform=�68   5A      5C      �� 5D   
   -	   5        $ 	lblHelper��5D      $��
Add Person��6#   $
��Left=�68   $��40��6S   $��
328,82,0,0q�6_   $	��Top=�6s   $��94��6�   ���	#FF1C8ADB$ 0.5,0.5&�6�   ��6�   Z� 6�   �� 5E   
   -
   5        $ txtbxAttachment��5E      $
��Left=�6+   $��23��6F   $��
149,40,0,0q�6R   $
 Wrap=�6f   $��Enter File Path Here��6z   $	��Top=�6�   $	��215��6�   ���	#FF2D2D30���	#FF93A6D9.��6  ��5F      �� 5G      $ 	#FF007ACC��6"   $		 0.9Q�64   .��5H   "   5I      �� 5J   
   -   5        $ lblPath��5J      $��Attachment Path:��6!   $
��Left=�6<   $��	43,40,0,0q�6W   $	��Top=�6j   $	��101��6�   $��23��6�   ���	#FF007ACC$��8(�6�   $
��Bold"�6�   ��5K      �� 5L      $ White��6"   $	 0.835Q�60   5N      �� 5O   
   -   5        $ 	btnAttach��5O      $��Attach��6$   $
��Left=�65   $��
369,40,0,0q�6P   $	��Top=�6d   $��75��6|   $��23��6�   ���	#FF707070���	#FF6B1913
�������6�   �� 5P   
   -   5        $ lblAttached��5P      $��File Attached��6%   $
��Left=�6=   $��
369,12,0,0q�6X   $	��Top=�6l   $��73��6�   
���  ��$��10(�6�   $
��Bold"�6�   $��23��6�   $y�Hidden=�6�   ��5Q      �� 5R      $ 	#FF0B1E02��6"   $		 0.7Q�64   5T      5U      5V      