

## Mercurial source control ##

http://www.vogella.de/articles/Mercurial/article.html

## Testing ##

Starting from scratch here and thus:

### Understanding the concept of tests, test suites ###

http://developer.android.com/reference/junit/framework/TestCase.html

http://marakana.com/tutorials/android/junit-test-example.html

### Testing android specific stuff ###

http://developer.android.com/guide/topics/testing/testing_android.html

### Sample of a test. Important, your test methods should start with "test"! ###

```
package com.advaton.calendar.common.Parser.test;

import com.advaton.calendar.common.Parser.CalendarParser;
import com.advaton.calendar.common.Parser.CalendarTokenType;
import junit.framework.TestCase;


public class CalendarParserTest extends TestCase {
	
	CalendarParser _parser;
	String _testRawString = "3pm meeting with John 2h -15";
	
	public CalendarParserTest(String test)
	{
		super(test);
	}
	
	protected void setUp() {
		_parser = new CalendarParser(_testRawString);
    }
	
	public void testWhenConstructed_Should_HaveRawValue()
	{
		assertEquals(_testRawString, _parser.get_RawValue());
		
	}
	public void testWhenConstructed_Should_HaveRawType()
	{
		assertEquals(CalendarTokenType.RAW, _parser.get_Type());
	}

}

```


## Obfuscation ##

http://proguard.sourceforge.net/

http://groups.google.com/group/android-developers/browse_thread/thread/dcc5808b002a47fc/b8c3850cc5f5e5dd?pli=1

## Dependency Injection ##
http://stackoverflow.com/questions/2135378/android-and-dependency-injection

## Java related ##

Understanding java date: http://www.odi.ch/prog/design/datetime.php

Regex in java: http://www.programmersheaven.com/2/RegexJAVA

The java equivalent of const: http://www.javamex.com/java_equivalents/const_java.shtml

Comparing strings in java: http://www.devdaily.com/java/edu/qanda/pjqa00001.shtml