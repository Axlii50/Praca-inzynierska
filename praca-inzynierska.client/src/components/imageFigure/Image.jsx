import styles from "./Image.module.css";

function Image({ children, imageSrc, imageAlt }) {
  return (
    <figure className={styles.figure}>
      <figcaption>{children}</figcaption>
      <img src={imageSrc} alt={imageAlt} />
    </figure>
  );
}

export default Image;
