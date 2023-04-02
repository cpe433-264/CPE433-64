import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'PM25 Checker',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
        // This makes the visual density adapt to the platform that you run
        // the app on. For desktop platforms, the controls will be smaller and
        // closer together (more dense) than on mobile platforms.
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: MyHomePage(title: 'Shanghai PM25 Level'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class PM25Reading {
  final int pm25;

  PM25Reading(this.pm25);

  factory PM25Reading.fromJson(Map<String, dynamic> json) {
    return PM25Reading(json['data']['iaqi']['pm25']['v']);
  }
}

class _MyHomePageState extends State<MyHomePage> {
  int _pm25Level = 0;

  Future<void> _fetchPM25Level() async {
    PM25Reading reading = await fetchPM25();
    setState(() {
      _pm25Level = reading.pm25;
    });
  }

  Future<PM25Reading> fetchPM25() async {
    final response = await http
        .get(Uri.https('api.waqi.info', '/feed/shanghai/', {'token': 'demo'}));
    if (response.statusCode == 200) {
      return PM25Reading.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('Failed to load data');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              'PM25 Level at Shanghai:',
            ),
            Text(
              '$_pm25Level',
              style: Theme.of(context).textTheme.headline4,
            ),
            Container(
                child: IconButton(
              icon: Icon(Icons.update),
              onPressed: _fetchPM25Level,
              alignment: Alignment.center,
            ))
          ],
        ),
      ),
    );
  }
}
