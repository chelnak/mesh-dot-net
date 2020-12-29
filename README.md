## Local testing notes

1. Install certs/mesh.pfx
2. Install python client

``` PowerShell
pip install mesh_client
```

3. Start python mock server

``` PowerShell
python -m mesh_client.mock_server
```

4. Test endpoint

``` PowerShell
$Cert = Get-Item -Path Cert:\LocalMachine\My\37AF35E3F13C65EA0D7BBAD148332924A1CE41D7

Invoke-RestMethod -uri https://localhost:8000/ -Certificate $Cert
```
