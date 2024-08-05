import Image from "../imageFigure/Image";
import Prompt from "../prompt/Prompt";
import styles from "./Main.module.css";

function Main() {
  return (
    <main className={styles.main}>
      <Prompt />

      <div className={styles.images}>
        <Image imageSrc="/public/bird.jpg" imageAlt="Image - before">
          Image - before
        </Image>

        <Image imageSrc="/public/woman.jpg" imageAlt="Image - after">
          Image - after
        </Image>
      </div>
    </main>
  );
}

export default Main;
