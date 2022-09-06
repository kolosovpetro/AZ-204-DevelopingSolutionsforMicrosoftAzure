# Lab 6: Authenticate by using OpenID Connect, MSAL, and .NET SDKs (Implement user authentication and authorization)
- Create active directory app using CLI
- Create password profile using azure powershell
- Get User principal name using azure powershell
- Implement AD authorization usin MVC

# Commands

- `az group create --name "az204-06-rg" --location "centralus"`
- `az ad app create --display-name "webappoidc" --available-to-other-tenants false`
- Application (client) ID: `ada3b1f1-b500-4e1e-a017-e3d744fdee7c`
- Directory (tenant) ID: `b40a105f-0643-4922-8e60-10fc1abf9c4b`
- Redirect URI: `https://localhost:44321/`
- Front-channel logout URL: `https://localhost:44321/signout-oidc`
- `Connect-AzureAD`
- `$aadDomainName = ((Get-AzureAdTenantDetail).VerifiedDomains)[0].Name`
- `$passwordProfile = New-Object -TypeName Microsoft.Open.AzureAD.Model.PasswordProfile`
- `$passwordProfile.Password = 'Pa55w.rd1234'`
- `$passwordProfile.ForceChangePasswordNextLogin = $false`
- `New-AzureADUser -AccountEnabled $true -DisplayName 'aad_lab_user1' -PasswordProfile $passwordProfile -MailNickName 'aad_lab_user1' -UserPrincipalName "aad_lab_user1@$aadDomainName"`
- New User ID: `733f66a5-c10b-4376-a759-41e7ad4aeba9`
- New User Name: `aad_lab_user1`
- Password: `Pa55w.rd1234`
- `(Get-AzureADUser -Filter "MailNickName eq 'aad_lab_user1'").UserPrincipalName`
- User principal name: `aad_lab_user1@kolosovp94gmail.onmicrosoft.com`
- Create MVC project: `dotnet new mvc --framework "netcoreapp3.1" --auth SingleOrg --client-id ada3b1f1-b500-4e1e-a017-e3d744fdee7c --tenant-id b40a105f-0643-4922-8e60-10fc1abf9c4b --domain https://localhost:44321`
- `dotnet dev-certs https --trust`
- Contoso TenantId: `7962bfbb-e817-4495-a371-b7328ae0d813`
- `Import-Module AzureAD.Standard.Preview`
- `AzureAD.Standard.Preview\Connect-AzureAD -TenantID '7962bfbb-e817-4495-a371-b7328ae0d813'`
- Contoso Tenant info: `kolosovp94@gmail.com AzureCloud  7962bfbb-e817-4495-a371-b7328ae0d813 razumovsky.onmicrosoft.com User`
- Contoso user info: `87615919-82c7-4598-9e14-5d26cc66a695 aad_lab_user2 aad_lab_user2@razumovsky.onmicrosoft.com Member`
- Contoso User UPN: `aad_lab_user2@razumovsky.onmicrosoft.com`


# Notes

- `<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="3.1.18" NoWarn="NU1605" />` -- keep an eye on this fckn `NoWarn="NU1605"`, without it solution does not compile