# gzip-multithread-compression

### Task

[Russian description](https://github.com/Altafard/test-gzip-multithread-compression/wiki/%D0%9F%D0%BE%D1%81%D1%82%D0%B0%D0%BD%D0%BE%D0%B2%D0%BA%D0%B0-%D0%B7%D0%B0%D0%B4%D0%B0%D1%87%D0%B8)

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
