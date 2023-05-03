using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using dxTestSolution.Module.BusinessObjects;

namespace ExtendModelEF.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        var cnt = ObjectSpace.GetObjects<Contact>().Count;
        if (cnt > 0) {
            return;
        }
        for (int i = 0; i < 5; i++) {
            var contact = ObjectSpace.CreateObject<Contact>();
            contact.FirstName = "FirstName" + i;
            contact.LastName = "LastName" + i;
            contact.Age = i * 10;
            contact.Type = i % 2 == 0;
            contact.TestInt = i * 100;
        }
        ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
