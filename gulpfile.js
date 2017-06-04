const gulp = require('gulp');
const sass = require('gulp-sass');
const debug = require('gulp-debug');
const watch = require('gulp-watch');


gulp.task('sass', function() {
    return gulp.src('css/*.sass')
        .pipe(debug({title: 'src'}))
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(gulp.dest('css/build'))
        .pipe(debug({title: 'dest'}));
})


gulp.task('watch', function() {
    gulp.watch('css/*.sass', ['sass']);
});
