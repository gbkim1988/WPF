   M S B A M L   `   `   `  ����  AsyncCommands    AsyncCommands.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��144��6   $	��360��6)    � 	Resources 5          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      � .+�5	   
   �� $��Stretch=�6'   $	��Top=�6E   $��23��6]   $��10,10,100,0q�6i   ��6   �� *Url+6i   �� 5
   
   $��Go��6:   $��Right=�6G   $	��Top=�6c   $��
10,10,10,0q�6{   $��75��6�   ��6   �� *CountUrlBytesCommand+6�   �� 5   
   $��
10,40,10,0q�6:   ��6   �� *
Operations+��5      �� 5      .R�5      � .+�5      �� ��6    �� *LoadingMessage+y�6C   �� *" Command.Execution.IsNotCompleted+ �� 	Converter# [  �� 5      ��6    �� *Command.Execution.Result+y�6M   �� *+)Command.Execution.IsSuccessfullyCompleted+# [  �� 5      
���  ����6    �� * Command.Execution.ErrorMessage+y�6S   �� *Command.Execution.IsFaulted+# [  6�   �� 5      $��Canceled��6    
����  �y�63   �� *Command.Execution.IsCanceled+# [  6�   �� 5      $��Cancel��6K   $��Right=�6\   $��Center=�6x   $��75��6�   $��0,0,30,0q�6�   ��6!   �� *Command.CancelCommand+6�   �� 5      $��X��6C   $��Right=�6O   $��Center=�6k   $��25��6�   ��6!   �� *RemoveCommand+6�   5      5      5      5      5      