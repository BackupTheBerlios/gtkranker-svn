SOURCES = \
./src/Main.cs \
./AssemblyInfo.cs \
./src/GUI/AboutDialog.cs \
./src/GUI/Mainwindow.cs \
./src/lib/libWebsites.cs \
./src/lib/GoogleService.cs \
./src/lib/libGoogleQuery.cs \
./src/lib/libConfig.cs \
./src/lib/libResults.cs \
./src/GUI/NewWebsite.cs

RESOURCES = \
./GTKRanker.glade \
./MonoIcon.png

RESOURCES_BUILD = $(foreach res,$(RESOURCES), $(addprefix -resource:,$(res)),$(notdir $(res)))

GAC_REFERENCES = \
gtk-sharp \
glib-sharp \
glade-sharp \
pango-sharp \
System.Drawing \
glade-sharp \
System.Web.Services \
gdk-sharp \
gecko-sharp \
System.Data \
Mono.Data.SqliteClient

GAC_REFERENCES_BUILD = $(addprefix /r:, $(GAC_REFERENCES))

ASSEMBLY_REFERENCES = \

ASSEMBLY_REFERENCES_BUILD = $(addprefix /r:, $(ASSEMBLY_REFERENCES))

PROJECT_REFERENCES = \

PROJECT_REFERENCES_BUILD = $(addprefix /r:, $(PROJECT_REFERENCES))

all: gtkRanker.exe

gtkRanker.exe: $(SOURCES) $(RESOURCES)
	mcs /target:exe /out:gtkRanker.exe $(RESOURCES_BUILD) $(GAC_REFERENCES_BUILD) $(ASSEMBLY_REFERENCES_BUILD) $(PROJECT_REFERENCES_BUILD) $(SOURCES)

clean:
	rm -f gtkRanker.exe
