# gzip-multithread-compression

### Setup

```cmd
git clone https://github.com/Altafard/gzip-multithread-compression.git
cd gzip-multithread-compression
msbuild /t:Build /p:Configuration=Release
cd release
gziptest
```

### Result

```
USAGE: GZipTest [command] [source] [destination]
where [command] is:
        compress        : compress source file and save it in destination path
        decompress      : decompress source file and save it in destination path
[source] and [destination]: paths to files
```