using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;

namespace Solution2.Module {
    public class Updater : ModuleUpdater {
        public Updater( Session session, Version currentDBVersion )
            : base( session, currentDBVersion ) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            User user1 = Session.FindObject<User>( new BinaryOperator( "UserName", "Sam" ) );
            if( user1 == null ) {
                user1 = new User( Session );
                user1.UserName = "Sam";
                user1.FirstName = "Sam";
                // Set a password if the standard authentication type is used
                user1.SetPassword( "" );
            }
            // If a user named 'John' doesn't exist in the database, create this user
            User user2 = Session.FindObject<User>( new BinaryOperator( "UserName", "John" ) );
            if( user2 == null ) {
                user2 = new User( Session );
                user2.UserName = "John";
                user2.FirstName = "John";
                // Set a password if the standard authentication type is used
                user2.SetPassword( "" );
            }

            BasicUser adminUser = Session.FindObject<BasicUser>( new BinaryOperator( "UserName", "Sam" ) );
            if( adminUser == null ) {
                adminUser = new BasicUser( Session );
                adminUser.UserName = "Sam";
                adminUser.FullName = "Sam";
            }
            // Make the user an administrator
            adminUser.IsAdministrator = true;
            // Set a password if the standard authentication type is used
            adminUser.SetPassword( "" );
            // Save the user to the database
            adminUser.Save();


            // If a role with the Administrators name doesn't exist in the database, create this role
            Role adminRole = Session.FindObject<Role>( new BinaryOperator( "Name", "Administrators" ) );
            if( adminRole == null ) {
                adminRole = new Role( Session );
                adminRole.Name = "Administrators";
            }
            // If a role with the Users name doesn't exist in the database, create this role
            Role userRole = Session.FindObject<Role>( new BinaryOperator( "Name", "Users" ) );
            if( userRole == null ) {
                userRole = new Role( Session );
                userRole.Name = "Users";
            }
            // Delete all permissions assigned to the Administrators and Users roles
            while( adminRole.PersistentPermissions.Count > 0 ) {
                Session.Delete( adminRole.PersistentPermissions[0] );
            }
            while( userRole.PersistentPermissions.Count > 0 ) {
                Session.Delete( userRole.PersistentPermissions[0] );
            }
            // Allow full access to all objects to the Administrators role
            adminRole.AddPermission( new ObjectAccessPermission( typeof( object ), ObjectAccess.AllAccess ) );
            // Allow editing the application model to the Administrators role
            adminRole.AddPermission( new EditModelPermission( ModelAccessModifier.Allow ) );
            // Save the Administrators role to the database
            adminRole.Save();
            // Allow full access to all objects to the Users role
            userRole.AddPermission( new ObjectAccessPermission( typeof( object ), ObjectAccess.AllAccess ) );
            //// Deny full access to the User type objects to the Users role
            //userRole.AddPermission(new ObjectAccessPermission(typeof(UserDetail), ObjectAccess.AllAccess, ObjectAccessModifier.Deny));
            // Deny full access to the Role type objects to the Users role
            userRole.AddPermission( new ObjectAccessPermission( typeof( Role ), ObjectAccess.AllAccess, ObjectAccessModifier.Deny ) );
            //userRole.AddPermission(new ObjectAccessPermission(typeof(), ObjectAccess.Read, ObjectAccessModifier.Deny));
            // Deny editing the application model to the Users role
            userRole.AddPermission( new EditModelPermission( ModelAccessModifier.Deny ) );
            // Save the Users role to the database
            userRole.Save();
            // Add the Administrators role to the user1
            user1.Roles.Add( adminRole );
            // Add the Users role to the user2
            user2.Roles.Add( userRole );
            // Save the users to the database
            user1.Save();
            user2.Save();
        }
    }
}
