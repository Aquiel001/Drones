# Drones
Drones Test

# Build and publish to IIS

## Previous requirements
- El SDK de .NET Core instalado en el equipo de desarrollo.
- Windows Server configurado con el rol de servidor Servidor web (IIS).

## Installing the .NET Core hosting suite
Install the .NET Core hosting suite on the IIS server. The bundle installs the .NET Core Runtime, the .NET Core Library, and the ASP.NET Core module. The module allows ASP.NET Core applications to run behind IIS.

## IIS site creation
1. On the IIS server, create a folder to contain the application's published files and folders. In a later step, the folder path is provided to IIS as the physical path to the application.

2. In IIS Manager, open the server node in the Connections panel. Right-click on the Sites folder. Click Add Website on the context menu.

3. Provide the Site Name and set the Physical Path to the application deployment folder you created. Provide the Link settings and select OK to create the website.

4. Confirm that the process model identity has the appropriate permissions.

If you change the default application pool identity (Process Model>Identity ) from ApplicationPoolIdentity to another identity, verify that the new identity has the necessary permissions to access the application folder, database, and other necessary resources. For example, the application pool requires read and write access to the folders where the application reads and writes files.

## Publish and deploy the app
Publishing an application means generating a compiled application that can be hosted on a server. Deploying an application means moving the published application to a hosting system. The publish step is handled by the .NET Core SDK, while the deploy step can be handled using different approaches. In this case the folder implementation approach is adopted, where:

- The app is published to a folder.
- The folder contents are moved to the IIS site folder (the physical path to the site in IIS Manager).

### Using .net core CLI
> 1. In a command shell, publish the application to Release Settings with the dotnet publish command:
> > dotnet publish --configuration Release
> 2. Move the contents of the bin/Release/{TARGET FRAMEWORK}/publish folder to the IIS site folder on the server, which is the physical path of the site in IIS Manager.

## Browse website
The application can be accessed in a browser after it receives the first request. Make a request to the application on the endpoint binding that you have established in IIS Manager for the site.
