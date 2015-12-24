#Create a New Strategy

# Introduction #

Adding new strategies is a bit awkward right now (in the process of improving) so please bear with me.  If anyone has any good ideas to improve the workflow, send me an e-mail or submit a bug report.


# Details #

When you first run the TI Robot, you will need to enter your trading details and define a new strategy.

## Entering Initial Details ##

Go to the "Login" tab and enter your Trade Ideas user name and password (you can start with "DEMO"/"demo") and the details of your Interactive Brokers TWS (typically: host "localhost" or "127.0.0.1" and port "7496").  If you go to TWS's API settings, you can set the simulator to run on a different port (say 7499) which will let you run both live and simulator at the same time, letting you test new strategies while continuing to run live ones.

You also need to enable TWS for API connections.  For details see http://ibtradingtools.com/stewie-configtws.html

Next go to "Settings" tab and set a quote timeout (the amount of time that the bot will wait for TWS to send the bid/ask/last data - the longer you wait, the more trades the bot will take, but the farther from the alert price the trade will become) and the local time for market open & close.

## Creating a new Strategy ##

Strategies were designed to be used with the Odds Maker (though this is not a necessity).  That means traders first start with a new alert window, test with the Odds Maker and then add the strategy for testing.  To do this:
  * Open the menu option "TradeIdeas > New Alert Window"
  * Configure the alert as you would normally do with Trade Ideas (right-click > "Configure")
  * If you have the Odds Maker, click "Backtest" and configure the entry/exit options
  * When you're satisfied, click "Add New" which will create a new strategy row in the Strategies tab.  When you see this, click "Cancel" to close the alerts window.
  * To configure the strategy parameters, select the strategy and click the "Edit..." button which will bring up the Strategy Definition dialog
  * Set the position sizing, entry/exit times, and other order details then click "OK"
  * To have the robot automatically trade this strategy, click the "Enable" check (the far-left column) and make sure the "Mode" column is set appropriately (start with Sim and then move to Live when you're convinced it works).  If you're using Sim, make sure the simulator settings in the "Settings" tab are configured to use your simulator account and not your live account!