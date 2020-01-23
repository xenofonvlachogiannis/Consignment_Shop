using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConsignmentShop
{
    public partial class ConsignemtShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        private decimal storeProfit = 0;
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();

        public ConsignemtShop()
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items;
            itemsListBox.DataSource = itemsBinding;

            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            vendorsBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorsBinding;

            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";

        }

        private void SetupData()
        {
            store.Name = "Second Books";

            store.Vendors.Add(new Vendor { FirstName = "Gina", LastName = "Jackson" });
            store.Vendors.Add(new Vendor { FirstName = "Michael", LastName = "Gerrard" });
            store.Items.Add(new Item
            {
                Title = "The Magicial",
                Description = "A book about magician",
                Price = 9.50M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about a whale",
                Price = 5.00M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Harry Potter Book 1",
                Description = "A book about a boy",
                Price = 8.50M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Lord of the Rings",
                Description = "A book about a world",
                Price = 11.30M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "The Great Gatsby",
                Description = "The quintessential Jazz Age novel",
                Price = 12.60M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Robinson Crusoe",
                Description = "The first English novel",
                Price = 7.90M,
                Owner = store.Vendors[1]
            });
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)itemsListBox.SelectedItem;
            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += item.Price * (decimal)item.Owner.Commission;
                storeProfit += item.Price * (1 - (decimal)item.Owner.Commission);
            }

            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            StoreProfitValue.Text = string.Format($" ${Math.Round(storeProfit,2)}");

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}
