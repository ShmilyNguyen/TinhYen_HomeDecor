<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="rptDashboard.aspx.cs" Inherits="WKS.DMS.WEB.Reports.rptDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>


        


  




        <table width="100%"  style="border:1px solid black;border-collapse:collapse;">
            <tr>
                <td colspan="2" align="center"  style="border:1px solid black;border-collapse:collapse;">
                    <strong>DASHBOARD</strong></td>
            </tr>
            <tr>
                <td  align="center"  style="border:1px solid black;border-collapse:collapse;">
                    TOP 5 SR</td>
                <td align="center"  style="border:1px solid black;border-collapse:collapse;">
                    GAUGE TARGET</td>
            </tr>
            <tr>
                <td  align="center"  style="border:1px solid black;border-collapse:collapse;">
                    <div id='table_div'></div></td>
                <td align="center"  style="border:1px solid black;border-collapse:collapse;">
                    <div id="chart_div5" >
                    </div>
                </td>
            </tr>
            <tr>
                <td  align="center"  style="border:1px solid black;border-collapse:collapse;">
                    PERFORMANCE BY REGION</td>
                <td  align="center"  style="border:1px solid black;border-collapse:collapse;">

                    COMPANY PERFORMANCE</td>
            </tr>
            <tr>
                <td style="border:1px solid black;border-collapse:collapse;">
                   <div id="chart_div" style="width: 600px; height: 400px;"></div></td>
                <td style="border:1px solid black;border-collapse:collapse;">
                    <div id="chart_div0" style="width: 600px; height: 400px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" style="border:1px solid black;border-collapse:collapse;">
                    SALE BY REGION</td>
                <td align="center" style="border:1px solid black;border-collapse:collapse;">
                    MONTHLY SALE BY REGION</td>
            </tr>
            <tr>
                <td style="border:1px solid black;border-collapse:collapse;">
                   <div id="chart_div3" style="width: 600px; height: 400px;">
                    </div></td>
                <td style="border:1px solid black;border-collapse:collapse;">
                    <div id="chart_div4" style="width: 600px; height: 400px;">
                    </div></td>
            </tr>
        </table>


        


  




    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.load('visualization', '1', { packages: ['gauge'] });
        google.load('visualization', '1', { packages: ['table'] });
        google.setOnLoadCallback(drawChart);
        google.setOnLoadCallback(drawChart2);
        google.setOnLoadCallback(drawChart3);
        google.setOnLoadCallback(drawChart4);
        google.setOnLoadCallback(drawChart5);
        google.setOnLoadCallback(drawTable1);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
              <%= arrData1 %>
            ]);

            var options = {
                title: 'Company Performance',
                hAxis: { title: 'Region', titleTextStyle: { color: 'red' } }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            chart.draw(data, options);

        };

        function drawChart2() {
            var data = google.visualization.arrayToDataTable([
              <%= arrData2 %>
            ]);

            var options = {
                title: 'Company Performance'
            };

            var chart = new google.visualization.LineChart(document.getElementById('chart_div0'));
            chart.draw(data, options);
        }


        function drawChart3() {
            var data = google.visualization.arrayToDataTable([
              <%= arrData3 %>
            ]);

            var options = {
                title: 'My Daily Activities'
            };

            var chart = new google.visualization.PieChart(document.getElementById('chart_div3'));
            chart.draw(data, options);
        }


        function drawChart4() {
            // Some raw data (not necessarily accurate)
            var data = google.visualization.arrayToDataTable([
             <%= arrData4 %>
            ]);

            var options = {
                title: 'Monthly Sales by Region',
                vAxis: { title: "Sales" },
                hAxis: { title: "Month" },
                seriesType: "bars",
                series: { 5: { type: "line" } }
            };

            var chart = new google.visualization.ComboChart(document.getElementById('chart_div4'));
            chart.draw(data, options);
        }

        function drawChart5() {
            var data = google.visualization.arrayToDataTable([
              ['Label', 'Value'],
              ['SALE', 80],
              ['DISPLAY', 55],
              ['OUTLET', 68]
            ]);

            var options = {
                width: 400, height: 120,
                redFrom: 90, redTo: 100,
                yellowFrom: 75, yellowTo: 90,
                minorTicks: 5
            };

            var chart = new google.visualization.Gauge(document.getElementById('chart_div5'));
            chart.draw(data, options);
        }


        function drawTable1() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('number', 'Total Score');
            data.addColumn('boolean', 'Target');
            data.addRows([
              ['Nguyen Van A', { v: 98.2, f: '98.2' }, true],
              ['Nguyen Van B', { v: 85.7, f: '85.7' }, true],
              ['Nugyen Thi C', { v: 73.8, f: '73.8' }, true],
              ['Tran Van D', { v: 22.5, f: '22.5' }, true],
              ['Tran Van E', { v: 21.5, f: '21.5' }, true]
            ]);

            var table = new google.visualization.Table(document.getElementById('table_div'));
            table.draw(data, { showRowNumber: true });
        }


        
    </script>

</asp:Content>
