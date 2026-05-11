# ASP.NET Framework runtime
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

# Set IIS folder
WORKDIR /inetpub/wwwroot

# Copy all files
COPY . .

# Expose IIS port
EXPOSE 80
