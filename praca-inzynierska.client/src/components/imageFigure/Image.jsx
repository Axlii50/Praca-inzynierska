import styles from "./Image.module.css";

function Image({ imageSrc, imageAlt, title, setImage }) {
  return (
    <figure className={styles.figure}>
      <figcaption>{title}</figcaption>
      <img src={imageSrc} alt={imageAlt} />
    </figure>
  );
}

export default Image;
