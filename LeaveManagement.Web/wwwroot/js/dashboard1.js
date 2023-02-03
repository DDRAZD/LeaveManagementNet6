// -------------------------------------------------------------------------------------------------------------------------------------------
// Dashboard 1 : Chart Init Js
// -------------------------------------------------------------------------------------------------------------------------------------------
$(function () {
  "use strict";

  // -----------------------------------------------------------------------
  // Sales summery
  // -----------------------------------------------------------------------
  var sales_summery = {
    series: [
      {
        name: "Iphone",
        data: [0, 5, 6, 8, 25, 9, 8, 24],
      },
      {
        name: "Ipad",
        data: [0, 3, 1, 2, 8, 1, 5, 1],
      },
    ],
    chart: {
      fontFamily: "Nunito Sans,sans-serif",
      height: 250,
      type: "area",
      toolbar: {
        show: false,
      },
    },
    grid: {
      show: true,
      borderColor: "rgba(0,0,0,.1)",
      xaxis: {
        lines: {
          show: true,
        },
      },
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
    colors: ["#4fc3f7", "#2962ff"],
    dataLabels: {
      enabled: false,
    },
    stroke: {
      curve: "smooth",
      width: 2,
    },
    markers: {
      size: 3,
      strokeColors: "transparent",
    },
    xaxis: {
      categories: ["1", "2", "3", "4", "5", "6", "7", "8"],
      labels: {
        style: {
          colors: [
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
          ],
        },
      },
    },
    yaxis: {
      labels: {
        style: {
          colors: [
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
            "#a1aab2",
          ],
        },
      },
    },
    tooltip: {
      x: {
        format: "dd/MM/yy HH:mm",
      },
      theme: "dark",
    },
    legend: {
      show: false,
    },
  };

  var chart_area_spline = new ApexCharts(
    document.querySelector(".sales-summery"),
    sales_summery
  );
  chart_area_spline.render();

  // -----------------------------------------------------------------------
  // email campaign
  // -----------------------------------------------------------------------
  var email_campaign = {
    series: [45, 27, 15, 18],
    labels: ["Open", "Un-opened", "Bounced", "Clicked"],
    chart: {
      type: "donut",
      height: 200,
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      width: 0,
    },
    plotOptions: {
      pie: {
        expandOnClick: true,
        donut: {
          size: "73",
          labels: {
            show: true,
            name: {
              show: true,
              offsetY: 10,
            },
            value: {
              show: false,
            },
            total: {
              show: true,
              fontSize: "13px",
              color: "#a1aab2",
              label: "Ratio",
            },
          },
        },
      },
    },
    colors: ["#eff3f7", "#fb8c00", "#3699ff", "#2962ff"],
    tooltip: {
      show: true,
      fillSeriesColor: false,
    },
    legend: {
      show: false,
    },
    responsive: [
      {
        breakpoint: 480,
        options: {
          chart: {
            width: 200,
          },
        },
      },
    ],
  };

  var chart_pie_donut = new ApexCharts(
    document.querySelector("#email-campaign"),
    email_campaign
  );
  chart_pie_donut.render();

  // -----------------------------------------------------------------------
  // revenue statics
  // -----------------------------------------------------------------------
  var ravenue = {
    series: [
      {
        name: "",
        data: [20, 55, 44, 30, 61, 48, 20],
      },
    ],
    chart: {
      type: "bar",
      height: 100,
      width: 100,
      toolbar: {
        show: false,
      },
      sparkline: {
        enabled: true,
      },
    },
    colors: ["#fff"],
    grid: {
      show: false,
    },
    plotOptions: {
      bar: {
        horizontal: false,
        startingShape: "flat",
        endingShape: "flat",
        columnWidth: "60%",
        barHeight: "100%",
      },
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 3,
      colors: ["transparent"],
    },
    xaxis: {
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        show: false,
      },
    },
    yaxis: {
      labels: {
        show: false,
      },
    },
    axisBorder: {
      show: false,
    },
    fill: {
      opacity: 1,
    },
    tooltip: {
      theme: "dark",
      style: {
        fontSize: "12px",
        fontFamily: '"Nunito Sans", sans- serif',
      },
      x: {
        show: false,
      },
      y: {
        formatter: undefined,
      },
    },
  };

  var chart_column_basic = new ApexCharts(
    document.querySelector("#ravenue"),
    ravenue
  );
  chart_column_basic.render();

  // -----------------------------------------------------------------------
  // Page views
  // -----------------------------------------------------------------------
  var views = {
    series: [
      {
        name: "Views : ",
        data: [6, 10, 9, 11, 9, 10, 12],
      },
    ],
    chart: {
      type: "area",
      height: 70,
      zoom: {
        enabled: false,
      },
      toolbar: {
        show: false,
      },
      sparkline: {
        enabled: true,
      },
    },
    grid: {
      show: false,
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      curve: "straight",
      width: 1,
      colors: ["rgba(255,255,255,.2)"],
    },
    xaxis: {
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        show: false,
      },
    },
    yaxis: {
      labels: {
        show: false,
      },
    },
    markers: {
      size: 0,
      strokeColors: "transparent",
      strokeWidth: 2,
      shape: "circle",
      colors: ["#fff"],
    },
    fill: {
      type: "gradient",
      gradient: {
        shadeIntensity: 1,
        opacityFrom: 0.5,
        opacityTo: 0.5,
        stops: [0, 90, 100],
      },
      colors: ["#fff", "#4fc3f7"],
    },
    tooltip: {
      theme: "dark",
      style: {
        fontFamily: '"Nunito Sans", sans- serif',
      },
      x: {
        show: false,
      },
      y: {
        formatter: undefined,
      },
      marker: {
        show: false,
      },
      followCursor: true,
    },
    legend: {
      show: false,
    },
  };

  var chart_area_basic = new ApexCharts(
    document.querySelector("#views"),
    views
  );
  chart_area_basic.render();

  // -----------------------------------------------------------------------
  // bounce rate
  // -----------------------------------------------------------------------
  var bouncerate = {
    series: [
      {
        name: "Rates : ",
        labels: ["2012", "2013", "2014", "2015", "2016", "2017"],
        data: [12, 19, 3, 5, 2, 3],
      },
    ],
    chart: {
      width: 150,
      height: 60,
      type: "line",
      toolbar: {
        show: false,
      },
      sparkline: {
        enabled: true,
      },
    },
    fill: {
      type: "solid",
      opacity: 1,
      colors: ["#2962ff"],
    },
    grid: {
      show: false,
    },
    stroke: {
      curve: "smooth",
      lineCap: "square",
      colors: ["#2962ff"],
      width: 3,
    },
    markers: {
      size: 3,
      colors: ["#2962ff"],
      strokeColors: "transparent",
      shape: "square",
      hover: {
        size: 7,
      },
    },
    xaxis: {
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        show: false,
      },
    },
    fill: {
      type: "solid",
      colors: ["#FDD835"],
    },
    yaxis: {
      labels: {
        show: false,
      },
    },
    tooltip: {
      theme: "dark",
      style: {
        fontFamily: '"Nunito Sans", sans- serif',
      },
      x: {
        show: false,
      },
      y: {
        formatter: undefined,
      },
      marker: {
        show: false,
      },
      followCursor: true,
    },
  };
  var chart_line_basic = new ApexCharts(
    document.querySelector("#bouncerate"),
    bouncerate
  );
  chart_line_basic.render();
});
