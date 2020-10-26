using System;
using System.Windows.Forms;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
//using xNet;

namespace BotSteam
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        public Form1()
        {
            InitializeComponent();
            Process();
        }
        string log = ""; //steam login
        string pas = ""; //steam pass

        string mlog = ""; //mail.ru login
        string mpas = ""; //mail.ru pass



        int i = 0;
        int getpoint()
        {
            
            int result = Convert.ToInt32(driver.FindElement(By.XPath("//div[@class='nav__button-container']/a[@class='nav__button nav__button--is-dropdown']/span[@class='nav__points']")).Text);
            textBox1.Text += "Сравнение номер " + i + ": баланс: " + result + ", стоимость: ";
            return result;
        }

        int getvalue(string abc)
        {
           string one = (driver.FindElement(By.XPath(abc)).Text);

            string result = null;

            for (int i = 1; i < one.Length; i++)
            {
                if(one[i] == 'P')
                {
                    break;
                }
                else
                {
                    result += one[i];
                }
            }
            textBox1.Text += result + Environment.NewLine;
            i++;
            return Convert.ToInt32(result);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }


        string keysteam(string yourtext)
        {
            string key = null;
            for (int i = 0; i < yourtext.Length; i++)
            {
                if ((yourtext[i] == 'u') && (yourtext[i + 1] == 'n') && (yourtext[i + 2] == 't') && (yourtext[i + 3] == ' ') && (yourtext[i + 4] == 'i'))
                {
                    key += yourtext[i + 14];
                    key += yourtext[i + 15];
                    key += yourtext[i + 16];
                    key += yourtext[i + 17];
                    key += yourtext[i + 18];
                    break;
                }
            }
            return key;
        }

        string keysteam2(string yourtext)
        {
            string key = null;
            for (int i = 0; i < yourtext.Length; i++)
            {
                if ((yourtext[i] == 'u') && (yourtext[i + 1] == 'n') && (yourtext[i + 2] == 't') && (yourtext[i + 3] == ' ') && (yourtext[i + 4] == 'i'))
                {
                    key += yourtext[i + 15];
                    key += yourtext[i + 16];
                    key += yourtext[i + 17];
                    key += yourtext[i + 18];
                    key += yourtext[i + 19];
                    break;
                }
            }
            return key;
        }

        string[] LinksPrice;
        string[] LinksButton;
        string MainUrl;
        string ButtonIn;

        private void button2_Click(object sender, EventArgs e)
        {
            
            LinksPrice = File.ReadAllLines("LinksPrice.txt");
            LinksButton = File.ReadAllLines("LinksButton.txt");
            MainUrl = "https://www.steamgifts.com/";
            ButtonIn = "//div[@class='sidebar__entry-insert']";

            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("--headless");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl(MainUrl);
            driver.FindElement(By.XPath("//div[@class='nav__button-container']/a[@class='nav__sits']")).Click();
            driver.FindElement(By.XPath("//input[@id='steamAccountName']")).SendKeys(log);
            driver.FindElement(By.XPath("//input[@id='steamPassword']")).SendKeys(pas);
            driver.FindElement(By.XPath("//input[@id='imageLogin']")).Click();

             IWebDriver mail = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            //IWebDriver mail = new ChromeDriver(chromeOptions);
            mail.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            mail.Manage().Window.Maximize();
            mail.Navigate().GoToUrl("https://mail.ru/");
            mail.FindElement(By.XPath("//div[@class='mailbox__row mailbox__step_first']/div[@id='mailbox:loginContainer']/input[@id='mailbox:login']")).SendKeys(mlog);
            mail.FindElement(By.XPath("//div[@class='mailbox__row mailbox__row_ i-clearfix']/label[@id='mailbox:submit']/input[@class='o-control']")).Click();
            mail.FindElement(By.Id("mailbox:password")).SendKeys(mpas);
            mail.FindElement(By.XPath("//div[@class='grid__lcol grid__lcol_mailbox i-pull-left']/div[@id='mailbox-container']/div[@id='mailbox']/form[@id='auth']/div[@class='mailbox__body']/div[@class='mailbox__row mailbox__row_ i-clearfix']/label[@id='mailbox:submit']/input[@class='o-control']")).Click();
            mail.FindElement(By.XPath("/html/body[@class='page g-default-font theme-default']/div[@id='app-canvas']/div[@class='application']/div[@class='application-mail']/div[@class='application-mail__overlay']/div[@class='application-mail__wrap']/div[@class='layout layout_size_m layout_type_2pane layout_left-size_58 layout_right-size_60 layout_bordered']/div[@class='layout__main-frame']/div/div[@class='layout__sidebar layout__sidebar_size-80']/div[@class='letter-list letter-list_has-letters']/div[@class='scrollable g-scrollable scrollable_bright scrollable_footer']/div[@class='scrollable__container']/div[@class='dataset-letters']/div[@class='draggable']/div[@class='dataset dataset_select-mode_off']/div[@class='dataset__items']/a[@class='llc js-tooltip-direction_letter-bottom js-letter-list-item llc_normal'][1]/div[@class='llc__container']/div[@class='llc__content']")).Click();
            string yourtext = mail.FindElement(By.TagName("body")).Text;
            textBox1.Text = keysteam(yourtext);


            try
            {
                driver.FindElement(By.XPath("//div[@id='authcode_entry']/div[@class='authcode_entry_box']/input[@id='authcode']")).SendKeys(keysteam(yourtext));
                driver.FindElement(By.XPath("//div[@id='auth_buttonset_entercode']/div[@class='auth_button leftbtn']")).Click();
                driver.FindElement(By.XPath("//div[@id='auth_buttonsets']/div[@id='auth_buttonset_success']/a[@id='success_continue_btn']")).Click();
            }
            catch
            {
                driver.FindElement(By.XPath("//div[@id='authcode_entry']/div[@class='authcode_entry_box']/input[@id='authcode']")).Clear();

                driver.FindElement(By.XPath("//div[@id='authcode_entry']/div[@class='authcode_entry_box']/input[@id='authcode']")).SendKeys(keysteam2(yourtext));
                driver.FindElement(By.XPath("//div[@id='auth_buttonset_entercode']/div[@class='auth_button leftbtn']")).Click();
                driver.FindElement(By.XPath("//div[@id='auth_buttonsets']/div[@id='auth_buttonset_success']/a[@id='success_continue_btn']")).Click();

            }
            for (int i = 0; i < LinksPrice.Length; i++)
            {
                try
                {
                    driver.Navigate().GoToUrl(MainUrl);
                    if (getpoint() > getvalue(LinksPrice[i]))
                    {
                        driver.FindElement(By.XPath(LinksButton[i])).Click();
                        driver.FindElement(By.XPath(ButtonIn)).Click();
                    }
                    else
                    {
                        if(getpoint() < 10)
                        {
                            mail.Quit();
                            driver.Quit();
                            Application.Exit();
                        }
                    }
                }
                catch { };
            }

            MessageBox.Show("Complete");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string [] LinksPrice = File.ReadAllLines("LinksPrice.txt");
            string [] LinksButton = File.ReadAllLines("LinksButton.txt");



        }


        public void Process()
        {
            LinksPrice = File.ReadAllLines("LinksPrice.txt");
            LinksButton = File.ReadAllLines("LinksButton.txt");
            MainUrl = "https://www.steamgifts.com/";
            ButtonIn = "//div[@class='sidebar__entry-insert']";

            var chromeOptions = new ChromeOptions();
            /*var firefoxOptions = new FirefoxOptions();*/


            //chromeOptions.AddArguments("--headless");

            /*driver = new FirefoxDriver(firefoxOptions);*/
            driver = new ChromeDriver(chromeOptions);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl(MainUrl);
            driver.FindElement(By.XPath("//div[@class='nav__button-container']/a[@class='nav__sits']")).Click();
            driver.FindElement(By.XPath("//input[@id='steamAccountName']")).SendKeys(log);
            driver.FindElement(By.XPath("//input[@id='steamPassword']")).SendKeys(pas);
            driver.FindElement(By.XPath("//input[@id='imageLogin']")).Click();

            /*IWebDriver mail = new FirefoxDriver(AppDomain.CurrentDomain.BaseDirectory);*/
            IWebDriver mail = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);

            //IWebDriver mail = new ChromeDriver(chromeOptions);
            mail.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            mail.Manage().Window.Maximize();
            mail.Navigate().GoToUrl("https://mail.ru/");
            mail.FindElement(By.XPath("//input[@id='mailbox:login-input']")).SendKeys(mlog);
            mail.FindElement(By.XPath("//div[@class='mailbox__row mailbox__row_ i-clearfix']/button[@id='mailbox:submit-button']/input[@class='o-control']")).Click();
            mail.FindElement(By.XPath("//input[@id='mailbox:password-input']")).SendKeys(mpas);
            /*mail.FindElement(By.Id("mailbox:password")).SendKeys(mpas);*/
            mail.FindElement(By.XPath("//input[@class='o-control']")).Click();
            mail.FindElement(By.XPath("/html/body[@class='page g-default-font theme-default']/div[@id='app-canvas']/div[@class='application']/div[@class='application-mail']/div[@class='application-mail__overlay']/div[@class='application-mail__wrap']/div[@class='layout layout_size_m layout_type_2pane layout_left-size_58 layout_right-size_60 layout_bordered']/div[@class='layout__main-frame']/div/div[@class='layout__sidebar layout__sidebar_size-80']/div[@class='letter-list letter-list_has-letters']/div[@class='scrollable g-scrollable scrollable_bright scrollable_footer']/div[@class='scrollable__container']/div[@class='dataset-letters']/div[@class='draggable']/div[@class='dataset dataset_select-mode_off']/div[@class='dataset__items']/a[@class='llc js-tooltip-direction_letter-bottom js-letter-list-item llc_normal'][1]/div[@class='llc__container']/div[@class='llc__content']")).Click();
            string yourtext = mail.FindElement(By.TagName("body")).Text;
            textBox1.Text = keysteam(yourtext);


            try
            {
                driver.FindElement(By.XPath("//div[@id='authcode_entry']/div[@class='authcode_entry_box']/input[@id='authcode']")).SendKeys(keysteam(yourtext));
                driver.FindElement(By.XPath("//div[@id='auth_buttonset_entercode']/div[@class='auth_button leftbtn']")).Click();
                driver.FindElement(By.XPath("//div[@id='auth_buttonsets']/div[@id='auth_buttonset_success']/a[@id='success_continue_btn']")).Click();
            }
            catch
            {
                driver.FindElement(By.XPath("//div[@id='authcode_entry']/div[@class='authcode_entry_box']/input[@id='authcode']")).Clear();

                driver.FindElement(By.XPath("//div[@id='authcode_entry']/div[@class='authcode_entry_box']/input[@id='authcode']")).SendKeys(keysteam2(yourtext));
                driver.FindElement(By.XPath("//form/div[@id='auth_buttonsets']/div[@id='auth_buttonset_incorrectcode']/div[@class='auth_button leftbtn']")).Click();
                driver.FindElement(By.XPath("//div[@id='auth_buttonsets']/div[@id='auth_buttonset_success']/a[@id='success_continue_btn']")).Click();

            }
            for (int i = 0; i < LinksPrice.Length; i++)
            {
                try
                {
                    driver.Navigate().GoToUrl(MainUrl);
                    if (getpoint() > getvalue(LinksPrice[i]))
                    {
                        driver.FindElement(By.XPath(LinksButton[i])).Click();
                        driver.FindElement(By.XPath(ButtonIn)).Click();
                    }
                    else
                    {
                        if (getpoint() < 10)
                        {
                            mail.Quit();
                            driver.Quit();
                            Application.Exit();
                        }
                    }
                }
                catch { };
            }

            MessageBox.Show("Complete");
        }
    }
}
