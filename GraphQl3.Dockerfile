FROM microsoft/dotnet:2.1-sdk as builder  
 
RUN mkdir -p /root/src/app/netcoreapp
WORKDIR /root/src/app/netcoreapp

# copy just the project file over
# this prevents additional extraneous restores
# and allows us to re-use the intermediate layer
# This only happens again if we change the csproj.
# This means WAY faster builds!
COPY GraphQl3/GraphQl3.csproj GraphQl3/
COPY ./NuGet.Config GraphQl3/
RUN dotnet restore ./GraphQl3/GraphQl3.csproj 

COPY GraphQl3 GraphQl3/
WORKDIR /root/src/app/netcoreapp/GraphQl3
RUN dotnet publish -c release -f netcoreapp2.1 -o published


FROM microsoft/dotnet:2.1-aspnetcore-runtime

WORKDIR /root/  
COPY --from=builder /root/src/app/netcoreapp/GraphQl3/published .
ENTRYPOINT ["dotnet", "GraphQl3.dll"]
