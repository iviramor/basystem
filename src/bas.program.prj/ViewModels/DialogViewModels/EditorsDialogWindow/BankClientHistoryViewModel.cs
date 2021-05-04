using bas.program.ViewModels.Base;
using bas.program.Views.DialogViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow
{
    public class BankClientHistoryViewModel : ViewModel
    {

        public BankClientHistoryViewModel()
        {
                
        }


        public void ShowBankClientWindow()
        {
            new BankClientHistoryWindow()
            {
                DataContext = this
            }.ShowDialog();
            
        }


    }
}
