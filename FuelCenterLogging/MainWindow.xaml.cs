using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModelLibrary;
using System.Collections;



namespace FuelCenterLogging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Data That will be used through session:
        // SalesDate, SalesDateID, 3 FuelData objects, Register Totals object, Network Totals, Rebates
        // UserData
        BusinessDate businessDate = new BusinessDate();

        // 3 Fuel Data Objects
        FuelData unleadedData = new FuelData();
        FuelData plusData = new FuelData();
        FuelData premiumData = new FuelData();

        Rebates RebateData = new Rebates();
        NetworkTotals NetworkTotalData = new NetworkTotals();
        RegisterTotalsData RegisterTotalData = new RegisterTotalsData();

        Dictionary<int, string> VendorsDict = new Dictionary<int, string>();


        public MainWindow()
        {
            InitializeComponent();
            SetupInitialData();
        }

        public void SetupInitialData()
        {

       
            // Set default Business Date
            //dpSalesDate.SelectedDate = DateTime.Parse("04/05/2015");
            dpSalesDate.DisplayDateStart = DateTime.Parse("12/30/2014");
            businessDate.GetBusinessDate((DateTime)dpSalesDate.DisplayDateStart);

            //Set up default hidden textblocks

            PopulateFormObjects(businessDate.SalesDateID);

            SetControlsData();

            lblResults.Text = "Sales Date: " + businessDate.SalesDateID + "<LineBreak/>";

        }
        
        private void PopulateFormObjects(int salesDateID)
        {
            // Use PopulateData class to retreive a bulk of the data.
            // then use the properties to get access to the data.
            PopulateData LoadDefaultData = new PopulateData(salesDateID);
            // Assign 3 fueldata objects
            // Load Gas SalesData
            // get list of Gas Sales
            AssignGasSales(LoadDefaultData.FuelDataAllList);

            RebateData = LoadDefaultData.RebatesData;

            NetworkTotalData = LoadDefaultData.NetworkTotalsData;

            RegisterTotalData = LoadDefaultData.RegisterData;

            VendorsDict = LoadDefaultData.VendorList; 

        }

        private void SetControlsData()
        {
            SetGasDataToControls();
            SetRegisterDataToControls();
            SetNetworkTotalsDataToControls();
            SetRebatesDataToControls();
        }

        private void SetGasDataToControls()
        {  // Unleaded
            txtUnleadedSold.Text = unleadedData.GallonsSold.ToString();
            txtPriceUnleaded.Text = unleadedData.Price.ToString();
            txtUnleadedTotal.Text = unleadedData.TotalDollarsSold.ToString();
            txtCostUnleaded.Text = unleadedData.CostOfGas.ToString();

            //plus
            txtPlusSold.Text = plusData.GallonsSold.ToString();
            txtPricePlus.Text = plusData.Price.ToString();
            txtPlusTotal.Text = plusData.TotalDollarsSold.ToString();
            txtCostPlus.Text = plusData.CostOfGas.ToString();

            //premium
            txtPremiumSold.Text = premiumData.GallonsSold.ToString();
            txtPricePremium.Text = premiumData.Price.ToString();
            txtPremiumTotal.Text = premiumData.TotalDollarsSold.ToString();
            txtCostPremium.Text = premiumData.CostOfGas.ToString();
        }

        private void SetRegisterDataToControls()
        {
            txtTotalIncome.Text = RegisterTotalData.TotalIncome.ToString();
            txtTotalCash.Text = RegisterTotalData.Cash.ToString();
            txtTotalCredit.Text = RegisterTotalData.CreditCard.ToString();
            txtPaidIn.Text = RegisterTotalData.PaidIn.ToString();
            txtPaidOut.Text = RegisterTotalData.PaidOut.ToString();
            txtCashDeposit.Text = RegisterTotalData.CashDropped.ToString();
            txtDescription.Text = RegisterTotalData.Notes.ToString();
        }

        private void SetNetworkTotalsDataToControls()
        {
            txtNetworkTotal.Text = NetworkTotalData.Total.ToString(); 
            txtFees.Text = NetworkTotalData.Fees.ToString();
            txtNetworkNet.Text = NetworkTotalData.TotalNet.ToString();
        }

        private void SetRebatesDataToControls ()
        {
            txtRebateAmount.Text = RebateData.Amount.ToString();
        }
        /// <summary>
        /// AssignGasSales will assign 3 seperate fueldata objects.  These objects would preferably be declared at the top level of the class.
        /// </summary>
        /// <param name="allfuelDataList"></param>
        private void AssignGasSales(List<FuelData> allfuelDataList)
        {
            foreach (FuelData fd in allfuelDataList)
            { 
                if (fd.FuelTypeCode==1)
                {
                    unleadedData = fd;
                }
                else if (fd.FuelTypeCode ==3)
                {
                    plusData = fd;
                }
                else if(fd.FuelTypeCode == 2)
                {
                   premiumData = fd;
                }
            }
        }

        private void UpdateAllForms(object sender, SelectionChangedEventArgs e)
        {
            businessDate = new BusinessDate();
            //businessDate.GetSalesDateIDBySalesDate(dpSalesDate.SelectedDate);
            //PopulateFormObjects();
        }
    }
}
