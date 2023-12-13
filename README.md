# StarCitizenTranslator

## Overview
This is a simple tool to translate the Star Citizen game into any language you want with the help of DeepL.
Use this just to get a basic translation and then improve it by hand as DeepL is not perfect.

## Features

- **File Input/Output**: The program accepts the path of an input file (in English) and an output file (for the target language).
- **Incremental Translation**: If the output file already exists, the program resumes translation from where it stopped, avoiding duplication of work.
- **Selective Translation**: Lines beginning with specific keywords (e.g., "Human", "vehicle_Name", "item_Name") are copied directly without translation, assuming they are proper nouns or identifiers that do not require translation.
- **Console Feedback**: Provides real-time feedback in the console for each line processed, including copying and translation actions.

## Prerequisites

- .NET Core SDK (compatible with the version used for development)
- Access to an internet connection (for online translation services)
- Adequate permissions to read and write files in the specified locations

## How to use
Configure the 'StarCitizenTranslator.dll.config' file with your DeepL API key and the language you want to translate to and if you want to use the DeepL Pro API.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="DEEPL_API_KEY" value="YOUR_API_KEY" />
		<add key="Free" value="true" />
		<add key="Language" value="DE" />
	</appSettings>
</configuration>
```

After that you can start the tool and it will ask for the english language file of Star Citizen and the path where you want to save the translated file.
It will then translate all the strings in the file and save it to the path you specified.

## How to get the english language file
You can get it from the [Star Citizen Localization GitHub repository](https://github.com/Dymerz/StarCitizen-Localization).
Or you can extract it yourself from the game files with the help of [StarFab](https://gitlab.com/scmodding/tools/starfab).

## Contributing

Contributions to improve StarCitizenTranslator are welcome. Please follow standard practices for submitting issues and pull requests on GitHub.