   M S B A M L   `   `   `  ����H  DAsyncCommands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:AsyncCommandsAsyncCommands  5   	       AsyncCommands.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5asyncCommandsclr-namespace:AsyncCommands     5   	     � Title$  
MainWindow��5   	   $	��144��6   $	��287��6)    � 	Resources 5          BooleanToVisibilityConverter-   'AsyncCommands.NullToVisibilityConverter  NullToVisibilityConverter��%:   &	        &	      �� 5   
   6'     5   
   62   5	      .��5
      � .+�5   
   �� $��Stretch=�6'   $	��Top=�6E   $��23��6]   $��10,10,10,10q�6i   ��6   �� *Url+6i   �� 5   
   $��Go��6:   $
��Left=�6G   $	��Top=�6b   $��10,38,10,10q�6z   $��75��6�   ��6   �� *CountUrlBytesCommand+6�   � 5   
   $
��Left=�6{   $	��Top=�6�   $��10,63,10,10q�6�   y�6   �� * CountUrlBytesCommand.Execution+ �� 	Converter# [ .+�5      �� $��
Loading...��6�   y�6   �� */-CountUrlBytesCommand.Execution.IsNotCompleted+# [  6�   �� 5      ��6   �� *'%CountUrlBytesCommand.Execution.Result+y�6N   �� *86CountUrlBytesCommand.Execution.IsSuccessfullyCompleted+# [  �� 5      
���  ����6   �� *-+CountUrlBytesCommand.Execution.ErrorMessage+y�6T   �� **(CountUrlBytesCommand.Execution.IsFaulted+# [  6�   5      5      5      