<p align="center">
    <img src="http://ordermaker.org/imgs/logo-cpq.png"
        height="100%">
</p>

<p align="center">
  <img alt="GitHub" src="https://img.shields.io/badge/licence-MIT-green">
  <img alt="GitHub" src="https://img.shields.io/badge/platform-.Net%205.0%20%7C%20Windows%20%7C%20IIS-blue">  
  <img alt="GitHub" src="https://img.shields.io/badge/database-MySql%208.00-blue">  
</p>

<hr>

MTD CPQManager helps to formulate commercial offers, select the correct configuration of the equipment supplied to the client, reduces the time for the formation of the proposal eliminating errors in the selection of equipment, improves the efficiency of distributors or sales agents.

⚠️ **This tool is part of the [MTD OrderMaker Server](https://github.com/olegbruev/OrderMakerServer) and does't work as a standalone solution. Both applications run on the same domain, e.g. order.example.com and cpq.example.com.**

## History

This solution was originally intended to create complicated proposals related to medical equipment, where there is an ultrasound mainframe that has lot of items such as electronic probes, licenses, accessories etc. All this has complex links and rules for creating commercial offers. The purpose of creating this tool was to simplify the work of sales managers and reduce errors in the preparation of commercial offers.

## Capabilities

<img src="http://ordermaker.org/imgs/cpq-ring.png" align="right" width="350"> 
     
- A single list of goods or services, standard and uniformity of the definitions.
- Import a price list from Excel and specify basic link rules.
- Simple, extended or Tech Data description of items.
- Differentiation of access rights to information.

Link Items Rule Builder:
- Disable or enable the ability to select an item
- Exclude an item when another item is selected
- Help message when selecting an item
- "OR OR" - select only one of group

## How to start

1. First of all, install the [MTD OrderMaker Server](https://github.com/olegbruev/OrderMakerServer).
3. Make sure the "CPQManagerLink" setting is "https://localhost:5003". If it doesn't, fix it and restart the app.
4. Open the "Users" menu and the "Accounts" module (list of users) and make sure your account have CPQ Role is Administrator.
5. Download the CPQ Manager [latest release](https://github.com/olegbruev/OrderMakerServer/releases) in the publish.zip file archive.
6. Unzip it and create file appsettings.json in same folder.  Use the appsettings.Template.json file to understand what settings you need to specify.
7. Run Mtd.Cpq.Manager.exe file. Then open browser and type address https://localhost:5003. If you are logged into the MTD OrderMaker Server and server is working on https://localhost:5001, then you authorization in CPQ Manager will be automatic.

⚠️ To add products you need to create at least one Group (DATA MANAGE -> GROUPS LIST) and create Title for quotes (SUPERVISION -> TITLES&TERMS).

## License

The CPQ Manager web application is free and open source software released under the MIT license.

## Project development
This solution is focused on a small group of customers, so application guides and background information will be added gradually if the community shows interest in this project. 

