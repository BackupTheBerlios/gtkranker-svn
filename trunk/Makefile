SOURCES = \
./Main.cs \
./AssemblyInfo.cs \
./GUI/AboutDialog.cs \
./GUI/Mainwindow.cs \
./lib/libWebsites.cs \
./lib/GoogleService.cs \
./lib/libGoogleQuery.cs \
./lib/libConfig.cs \
./lib/libResults.cs \
./GUI/NewWebsite.cs

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
gnome-sharp \
System.Web.Services \
gdk-sharp \
gecko-sharp

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
