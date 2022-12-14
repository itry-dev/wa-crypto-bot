# WhatsApp Crypto Bot

A simple WhatsApp Business Bot that fetch data for any crypto you like.  
Api served from Livecoinwatch.

Steps required:
-have a long term token from Facebook  
[get token](https://developers.facebook.com/docs/whatsapp/business-management-api/get-started#) 
-setting up an app on Facebook Developers activating WhatsApp functions  
[facebook app settings](https://business.facebook.com/settings/system-users/100081799923228?business_id=517996726704148) 
-checking the created access token at [developers.facebook](https://developers.facebook.com/tools/debug/accesstoken)  
-migrate database  
Add-Migration init_sqlite_db -StartupProject WA.Infrastructure -v  
Update-Database -StartupProject WA.Infrastructure -v  

Send any command to your bot, a guide will show you how to use the bot in case of a wrong command.

---

Per avere un token che non ha scadenza:  
[primo passaggio](https://developers.facebook.com/docs/whatsapp/business-management-api/get-started#)  

Creare un system user. Assegnare un assets. Un assets in questo caso significa scegliere la voce **Apps** dalla finestra che si apre scegliendo aggiungi assets. Come permessi dare full control. Successivamente, premete genera access token e selezione le voci che comprendono whatsapp nel nome.  
[secondo passaggio](https://business.facebook.com/settings/system-users/100081799923228?business_id=517996726704148)  
 
Controllo dell'access token creato  
[terzo passaggio, controllo access token generato](https://developers.facebook.com/tools/debug/accesstoken)

## Risorse tecniche
Lanciare le migrazioni  
Add-Migration init_sqlite_db -StartupProject WA.Infrastructure -v  
Update-Database -StartupProject WA.Infrastructure -v  

Invia qualsiasi testo al bot, ti invierà una guida all'utilizzo nel caso di comando non riconosciuto.
