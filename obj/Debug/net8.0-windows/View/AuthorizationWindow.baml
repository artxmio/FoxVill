   M S B A M L   `   `   `  ����B  >FoxVill, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FoxVill.ViewFoxVill.View  5   	   &     FoxVill.View.AuthorizationWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FoxVill.View     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.2.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  AuthorizationWindow��5	   	    � WindowState$ 	Maximized=�5
   	   $
��1080��5   	   $
��1920��5   	    �� BitmapScalingMode$ HighQuality=�5   	    � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	   $	��800��5   	   $
��1150��5   	   ��5      �� 5   
    �� ImageSource$) #/Resources/Images/Png/auth_back.png��6    j� Stretch$
 Fill=�6G    � 	Resources 5         BaseTextBlockStyle �� 
TargetType	 �� Property
 �� Value  BaseTextBoxStyle �� 	Resources �� Triggers P� Property P� Value  BaseButtonStyle   MaterialDesignFlatDarkButton �� BasedOn+  %MaterialDesignThemes.Wpf.ButtonAssist��%?  &	        &	 �     &	 /    7  �� 5   
   " ��6+   .�5      �� 	 � ��$P
 Jpack://application:,,,/FoxVill;component/Resources/Fonts/Caveat.ttf#Caveat)�6+   �� 5      	 � ��$
 25(�6)   �� 5      	 � ��$

 Bold"�6+   5      �� 5    
   " ��6)    5!      ���� 5"      '��       " ��6   .�5#      �� 	 �@��CornerRadius$
 15��65   5$      5%      .�5&      �� 	 � ��$
 	10 0 10 0q�6'   �� 5'      	 � ��$

 Auto��6'   �� 5(      	 � ��$
 37(�6)   �� 5)      	 �@��SelectionBrush

 �����6/   �� 5*      	 � ��$P
 Jpack://application:,,,/FoxVill;component/Resources/Fonts/Caveat.ttf#Caveat)�6+   �� 5+      	 � ��$

 Bold"�6+    5-      P� 5.       �@J�IsKeyboardFocusWithin$
 True��6;   . �5/      �� 	 � ��

 �   �64   50      52      �� 54   
   " ��6(   8   .�55      �� 	 � ��$	
 255��6&   �� 56      	 � ��$
 62��6'   �� 57      	 � ��

 �����6+   �� 58      	 � ��

 �   �6+   �� 59      	 � ��

 �   �6,   �� 5:      	 � ��$
 2q�60   �� 5;      	 �@ CornerRadius$
 15��6I   �� 5<      	 � ��$

 Bold"�6+   �� 5=      	 � ��$
 35(�6)   �� 5>      	 � ��$P
 Jpack://application:,,,/FoxVill;component/Resources/Fonts/Caveat.ttf#Caveat)�6+   5?      5@      .��5B      �  � RowDefinitions 5C   
   �� 5D      $
��Auto �6   �� 5E      .+�5H   
   ��  �� Orientation$ 
Horizontal=�6   $��Right=�5I      $	��Top=�5J      $��10q�5K      .+�5M      �� -   5         ��Name$ MinimizedButton��5M      
������ $��50��5O      $��50��5P      $��0q�5Q      $��0q�5R      ��5U      �� 5V      " ��6   # [ 60    5W      P� 5X       �@J�IsMouseOver$
 True��6=   . �5Y   "   �� 	 � ��

 �  ��6?   5Z      5\      .��5_      �� $��30��6   $%��/Resources/Images/Png/minus.png��5`      5a      �� 5c      -   5        $ ToggleButton��5c      
������ $��50��5e      $��50��5f      $��0q�5g      $��0q�5h      ��5k      �� 5l      " ��6   # [ 60    5m      P� 5n       �@J�IsMouseOver$
 True��6=   . �5o   "   �� 	 � ��

 �  ��6?   5p      5r      .��5u      �� $��27��6   $'��!/Resources/Images/Png/dwindow.png��5v      5w      �� 5y      -   5        
������ $��50��5z      $��50��5{      $��0q�5|      $��0q�5}      ��5�      �� 5�      " ��6   # [ 60    5�      P� 5�       �@J�IsMouseOver$
 True��6=   . �5�   "   �� 	 � ��

 �  ��6?   5�      5�      .��5�      �� $��35��6   $!��/Resources/Images/Png/x.png��5�      5�      5�      �� 5�   
   #��[  6   $��FoxVill��5�      $	��120(�5�      $��Center=�5�      $��0��5�      $��0��5�       � 5�   
   -   5          �Name$ 	MainFrame��5�      $��1��5�        � NavigationUIVisibility$ Hidden=�5�      $
��Auto��5�      $
��Auto��5�      5�      5�      